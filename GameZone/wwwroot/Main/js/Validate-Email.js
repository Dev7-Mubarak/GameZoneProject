function validateEmail() {
    const email = document.getElementById("email").value;
  
    if (email.includes("Admin")) {
        window.location.href = "/Dahboard/AdminIndex.html"; 
    } else if (email.includes("Owner")) 
        window.location.href = "/Dahboard/owner-station.html";
    
    else if (email.includes("User")) {
        window.location.href = "index-user.html";
    } else {
      alert("Invalid email address.");
    }
    
  }