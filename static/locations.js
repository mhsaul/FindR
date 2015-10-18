function validateForm() {
    var n = document.forms["newLocation"]["form-name"].value;
    var description = document.forms["newLocation"]["form-desc"].value;
    var t = document.forms["newLocation"]["type"].value;
    if (!objectPlaced || n == null || n == "" || description == null || description == "" || t == null || t == "") {
        //alert("Please enter all information (Name, Description, Type, Location).");
        alert("Please enter all information");
        return false;
    }
    else {
        if (t == "0")
            t = "bathroom";
        else if (t == "1")
            type = "water";
        else if (t == "2")
            t = "cycling";
        else
            t = "wifi";
        post("http://173.250.206.173:8080/findR/php/input/postLocation.php", [{'lat': markerLat}, {'long': markerLng}, {'type': t}, {'name': n}, {'details': description}], 'post');
    }
}


var x = document.getElementById("demo");
  
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(initMap);
    } else { 
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function newLocation() {
    alert("HELLO")
}

function findLocations() {
    alert("HELLO")
}

function post(path, params, method) {
    method = method || "post"; // Set method to post by default if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("id", "myForm");
    form.setAttribute("method", method);
    form.setAttribute("action", path);
    var length = params.length;
    for (var i = 0; i < length; i++) {
        for(var key in params[i]) {
            if(params[i].hasOwnProperty(key)) {
                console.log(key);
                console.log(params[i][key]);
                var hiddenField = document.createElement("input");
                hiddenField.setAttribute("type", "text");
                hiddenField.setAttribute("name", key);
                hiddenField.setAttribute("value", params[i][key]);

                form.appendChild(hiddenField);
             }
        }
    }
    var submitButton = document.createElement("button");
    submitButton.setAttribute("type", "submit");
    form.appendChild(submitButton);
    document.body.appendChild(form);
}

function httpGetAsync(theUrl, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function() { 
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", theUrl, true); // true for asynchronous 
    xmlHttp.send(null);
}

function Location(latitude, longitude, type) {
    this.lat = latitude;
    this.lon = longitude;
    this.type = type;
}
