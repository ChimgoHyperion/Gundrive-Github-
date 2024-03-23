window.addEventListener('load', function() {
    setTimeout(function() {
      document.getElementById('loadingScreen').style.display = 'none';
      document.getElementById('content').style.display = 'block';
    }, 3000); // Change 3000 to the desired duration of the loading screen in milliseconds
  });
  
  document.getElementById("downloadButton").addEventListener("click", function() {
    // Replace the URL with the actual download link
    window.location.href = "https://drive.google.com/file/d/1PC4R8_7fcGPbyQbnwzabcxYa_1tznarL/view?usp=drivesdk";
  });
  