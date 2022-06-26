var MongoClient = require('mongodb').MongoClient;
var url = "mongodb+srv://admin:passpass@cluster0.za92g.mongodb.net/test";

MongoClient.connect(url, function (err, db) {
    if (err)
        throw error;
    var dbo = db.db("test");
    dbo.collection("itemposts").find({}).toArray(function (err, result) {
        if (err)
            throw err;
        console.log(result);
        db.close();
    });
});

