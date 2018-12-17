from elasticsearch import Elasticsearch
from flask import Flask,jsonify
from flask import json
from flask import request
from flask import Response


app = Flask(__name__)

es = Elasticsearch()

def create_product(product):
    doc = product
    try:
        for jsonDoc in doc:
            res = es.index(index="products-index", doc_type='products', body=jsonDoc)

        resp = Response('İşlem Başarılı', status=200, mimetype='application/json')
        return resp
    except Exception as  ex:
        return Response(str(ex),status=200,mimetype='application/json');

def search(es_object, index_name, search):
    return es_object.search(index=index_name, body=search)

@app.route("/")
def index():
    try:
        search_object = {'query': {'match': {'Name': request.args.get('q')}}}
        results = search(es, 'products-index', json.dumps(search_object))
        #results = es.search(index="products-index", body={"query": {"match_all": {}}})
        return Response(jsonify(results),status=200,mimetype='application/json');
    except Exception as ex:
        return Response(str(ex),status=200,mimetype='application/json');
    

@app.route('/create', methods = ['POST'])
def create():
    res = create_product(json.loads(request.data))
    return res;
    

if __name__ == "__main__":
    app.run(port=5001)