from datetime import datetime
from elasticsearch import Elasticsearch
from flask import Flask
from flask import json
from flask import request
from flask import Response

app = Flask(__name__)

es = Elasticsearch()

def create_product(product):
    doc = product
    res = es.index(index="product-index", doc_type='product', body=doc)
    resp = Response(json.dumps(res), status=200, mimetype='application/json')
    return resp

@app.route("/")
def index():
    return "Hi there, this is a web api"

@app.route('/create', methods = ['POST'])
def create():
    return create_product(json.dumps(request.json))
    

if __name__ == "__main__":
    app.run(port=5001)