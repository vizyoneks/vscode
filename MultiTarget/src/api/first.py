from datetime import datetime
from elasticsearch import Elasticsearch
from flask import Flask

app = Flask(__name__)

es = Elasticsearch()

@app.route("/")
def Index():
    TestElastic()
    return "Hi there, this is a web api"

def TestElastic():
    doc = {
        'author': 'seyfi',
        'text': 'Elasticsearch: cool. bonsai cool.',
        'timestamp': datetime.now(),
    }
    res = es.index(index="test-index", doc_type='tweet', id=2, body=doc)
    print("1 -> "+res['result'])

    res = es.get(index="test-index", doc_type='tweet', id=2)
    print(res['_source'])

    es.indices.refresh(index="test-index")

    res = es.search(index="test-index", body={"query": {"match_all": {}}})
    print("Got %d Hits:" % res['hits']['total'])
    for hit in res['hits']['hits']:
        print("%(timestamp)s %(author)s: %(text)s" % hit["_source"])

if __name__ == "__main__":
    app.run(port=5001)
    