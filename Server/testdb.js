
var mongoose = require('mongoose');

mongoose.connect("mongodb://localhost/testdb", { useNewUrlParser: true });

var Schema = mongoose.Schema;

var ChatLineSchema = new Schema({
    username: String,
    chatLine: String
});

// Link Schema to collection.

var ChatLine = mongoose.model('ChatLine', ChatLineSchema);

// Make a chatline. Collection is automatically created. 

ChatLine.create(
    {
        username: "user",
        chatLine: "chatting"
    },
    (error, chatLine) => {
        console.log("Error was: " + error);
        console.log(chatLine);
    }
);