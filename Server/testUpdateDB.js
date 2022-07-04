// import { MongoClient } from "mongodb";
var MongoClient = require('mongodb').MongoClient;
var ObjectId = require('mongodb').ObjectID;

// Replace the uri string with your MongoDB deployment's connection string.
const uri = "mongodb+srv://admin:passpass@cluster0.za92g.mongodb.net/test";

const client = new MongoClient(uri);

async function run() {
  try {
    await client.connect();

    const database = client.db("test");
    const movies = database.collection("itemposts");

    // create a filter for a movie to update
    // const filter = { title: "Random Harvest" };
    // const filter = { itemNickName: "James" };
    const filter = { "_id" : ObjectId("4ecc05e55dd98a436ddcc47c") };

    // this option instructs the method to create a document if no documents match the filter
    // const options = { upsert: true };

    // create a document that sets the plot of the movie
    const updateDoc = {
      $set: {
        itemNickName: `test123`
      },
    };

    // const result = await movies.updateOne(filter, updateDoc, options);
    const result = await movies.updateOne(filter, updateDoc);

    console.log(
      `${result.matchedCount} document(s) matched the filter, updated ${result.modifiedCount} document(s)`,
    );

  } finally {
    await client.close();
  }
}
run().catch(console.dir);
