from flask import Flask
import os
app = Flask(__name__)


instance = os.environ['INSTANCE']

@app.route("/")
def hello():
    return "Hello world from instance: {}\n".format(instance)

if __name__=="__main__":
    app.run(host="0.0.0.0")