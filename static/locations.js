function validateForm() {
    var name = document.forms["newLocation"]["form-name"].value;
    var description = document.forms["newLocation"]["form-desc"].value;
    var type = document.forms["newLocation"]["type"].value;
    if (!objectPlaced || name == null || name == "" || description == null || description == "" || type == null || type == "") {
        //alert("Please enter all information (Name, Description, Type, Location).");
        alert("Bitch, don't even try that shit");
        return false;
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

function showPosition(position) {
    position.coords.latitude;
    position.coords.longitude;  
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
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for(var key in params) {
        if(params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);

            form.appendChild(hiddenField);
         }
    }

    document.body.appendChild(form);
    form.submit();
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
