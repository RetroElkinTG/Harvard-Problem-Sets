import os

from cs50 import SQL
from flask import Flask, flash, redirect, render_template, request, session
from flask_session import Session
from werkzeug.security import check_password_hash, generate_password_hash

from helpers import apology, login_required, lookup, usd

# Configure application
app = Flask(__name__)

# Custom filter
app.jinja_env.filters["usd"] = usd

# Configure session to use filesystem (instead of signed cookies)
app.config["SESSION_PERMANENT"] = False
app.config["SESSION_TYPE"] = "filesystem"
Session(app)

# Configure CS50 Library to use SQLite database
db = SQL("sqlite:///finance.db")


@app.after_request
def after_request(response):
    """Ensure responses aren't cached"""
    response.headers["Cache-Control"] = "no-cache, no-store, must-revalidate"
    response.headers["Expires"] = 0
    response.headers["Pragma"] = "no-cache"
    return response


@app.route("/")
@login_required
def index():
    """Show portfolio of stocks"""

    if request.method == "POST" or request.method == "GET":

        # Cash Amount
        rows = db.execute("SELECT cash FROM users WHERE id = ?", session["user_id"])
        cash = rows[0]['cash']

        # Stocks Table
        table_name = f"user_purchases_{session['user_id']}"
        query = f"SELECT * FROM {table_name}"
        purchases = db.execute(query)

        # Total Value
        total = cash
        for purchase in purchases:
            price = purchase['price'].replace("$", '')
            price = float(price.replace(",", ''))
            total += price

        return render_template("index.html", purchases=purchases, cash=usd(cash), total=usd(total))


@app.route("/buy", methods=["GET", "POST"])
@login_required
def buy():
    """Buy shares of stock"""

    # When form is submitted via POST
    if request.method == "POST":

        symbol = request.form.get("symbol")
        if not symbol:
            return apology("must provide symbol", 403)

        amount = request.form.get("shares")
        if not amount:
            return apology("Invalid number")
        try:
            amount = int(amount)
        except ValueError:
            return apology("Invalid number")
        if amount < 1:
            return apology("Invalid number")

        quote = lookup(symbol)
        if not quote:
            return apology(symbol + " doesn't exist")

        # Create new table of purchased stock
        table_name = f"user_purchases_{session['user_id']}"

        # Add to the purchases table
        rows = db.execute("SELECT cash FROM users WHERE id = ?", session["user_id"])
        cash = rows[0]['cash']
        price = round(quote['price'] * amount, 2)
        if cash < price:
            return apology("Not enough funds")
        add_to_table = f"INSERT INTO {table_name} (symbol, amount, price) VALUES(?, ?, ?)"
        db.execute(add_to_table, symbol, amount, usd(price))
        new_cash = cash - price
        db.execute("UPDATE users SET cash = ? WHERE id = ?", new_cash, session["user_id"])

        # Render buy page
        return render_template("buy.html", price=price)

    # User reached route via GET (as by clicking a link or via redirect)
    else:
        return render_template("buy.html", price=0.00)


@app.route("/history")
@login_required
def history():
    """Show history of transactions"""
    return apology("TODO")


@app.route("/login", methods=["GET", "POST"])
def login():
    """Log user in"""

    # Forget any user_id
    session.clear()

    # User reached route via POST (as by submitting a form via POST)
    if request.method == "POST":
        # Ensure username was submitted
        if not request.form.get("username"):
            return apology("must provide username", 403)

        # Ensure password was submitted
        elif not request.form.get("password"):
            return apology("must provide password", 403)

        # Query database for username
        rows = db.execute(
            "SELECT * FROM users WHERE username = ?", request.form.get("username")
        )

        # Ensure username exists and password is correct
        if len(rows) != 1 or not check_password_hash(
            rows[0]["hash"], request.form.get("password")
        ):
            return apology("invalid username and/or password", 403)

        # Remember which user has logged in
        session["user_id"] = rows[0]["id"]

        # Redirect user to home page
        return redirect("/")

    # User reached route via GET (as by clicking a link or via redirect)
    else:
        return render_template("login.html")


@app.route("/logout")
def logout():
    """Log user out"""

    # Forget any user_id
    session.clear()

    # Redirect user to login form
    return redirect("/")


@app.route("/quote", methods=["GET", "POST"])
@login_required
def quote():
    """Get stock quote."""

    # When form is submitted via POST
    if request.method == "POST":

        symbol = request.form.get("symbol")
        if not symbol:
            return apology("Invalid symbol")

        quote = lookup(symbol)
        if not quote:
            return apology(symbol + " doesn't exist")

        # Render quote page
        quote['price'] = usd(quote['price'])
        return render_template("quote.html", quote=quote)

    # User reached route via GET (as by clicking a link or via redirect)
    else:
        return render_template("quote.html", quote={"price": usd(0), "symbol": "None"})


@app.route("/register", methods=["GET", "POST"])
def register():
    """Register user"""

    # When form is submitted via POST, check for possible errors and insert the new user in the USERS table
    if request.method == "POST":

        username = request.form.get("username")
        if not username:
            return apology("Invalid username")

        password = request.form.get("password")
        if not password:
            return apology("Invalid password")

        confirmation = request.form.get("confirmation")
        if not confirmation:
            return apology("Invalid password confirmation")

        if password != confirmation:
            return apology("Passwords don't match")

        if db.execute("SELECT * FROM users WHERE username = ?", username) != []:
            return apology("Username already taken")

        # Log user in

        hash = generate_password_hash(password)
        db.execute("INSERT INTO users (username, hash) VALUES(?, ?)", username, hash)
        rows = db.execute("SELECT * FROM users WHERE username = ?", username)
        session["user_id"] = rows[0]["id"]
        table_name = f"user_purchases_{session['user_id']}"
        create_table = f"CREATE TABLE {table_name} (symbol TEXT NOT NULL, amount INT NOT NULL, price TEXT NOT NULL)"
        db.execute(create_table)

        return redirect("/")

    # User reached route via GET (as by clicking a link or via redirect)
    else:
        return render_template("register.html")


@app.route("/sell", methods=["GET", "POST"])
@login_required
def sell():
    """Sell shares of stock"""

    # When form is submitted via POST
    if request.method == "POST":

        symbol = request.form.get("symbol")
        if not symbol:
            return apology("must provide symbol", 403)

        sell_amount = request.form.get("amount")
        if not sell_amount:
            return apology("must provide amount", 403)
        sell_amount = int(sell_amount)
        if sell_amount < 1:
            return apology("Invalid number")

        quote = lookup(symbol)
        if not quote:
            return apology(symbol + " doesn't exist")

        # Check if purchases table has symbol and amount
        table_name = f"user_purchases_{session['user_id']}"
        find_table = f"SELECT amount FROM {table_name} WHERE symbol = '{symbol}'"
        amount = db.execute(find_table)[0]['amount']

        # Remove from the purchases table
        rows = db.execute("SELECT cash FROM users WHERE id = ?", session["user_id"])
        cash = rows[0]['cash']
        price = quote['price'] * amount
        sell_price = quote['price'] * sell_amount
        if sell_amount > amount:
            return apology("Cannot sell amount")
        update_price = f"UPDATE {table_name} SET amount = ?, price = ? WHERE symbol = ?"
        new_amount = amount - sell_amount
        new_price = price - sell_price
        new_cash = cash + price
        db.execute(update_price, new_amount, usd(new_price), symbol)
        db.execute("UPDATE users SET cash = ? WHERE id = ?", new_cash, session["user_id"])

        # Render quote page
        return redirect("/")

    # User reached route via GET (as by clicking a link or via redirect)
    else:
        return render_template("sell.html")
