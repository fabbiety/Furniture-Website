



//$(document).ready(function(){
//  // Activate Carousel with a specified interval
//  $("#myCarousel").carousel({interval: 500});
        
//  // Enable Carousel Indicators
//  $(".item1").click(function(){
//    $("#myCarousel").carousel(0);
//  });
//  $(".item2").click(function(){
//    $("#myCarousel").carousel(1);
//  });
//  $(".item3").click(function(){
//    $("#myCarousel").carousel(2);
//  });
    
//  // Enable Carousel Controls
//  $(".carousel-control-prev").click(function(){
//    $("#myCarousel").carousel("prev");
//  });
//  $(".carousel-control-next").click(function(){
//    $("#myCarousel").carousel("next");
//  });
//});



var video = document.getElementById("myvideo");
var playbtn = document.getElementById("playbttn");
var stopbtn = document.getElementById("stopbttn");
var slider = document.getElementById("slider");



playbtn.addEventListener("click", playVideo);
stopbtn.addEventListener("click", stopVideo);
slider.addEventListener("change", dragSlider);
slider.addEventListener("mouseup", playWhenDragStopsdrag);
slider.addEventListener("mousedown", pauseWhenDraging)
video.addEventListener("timeupdate", moveSlider);

var presentTime = video.currentTime();
var minutes = Math.floor(presentTime / 60);
var seconds = Math.floor((presentTime - minutes) * 60);



function playVideo() {
    if (video.paused) {
        video.play();
        playbtn.setAttribute("src", "/Images/contactUs/pause.png");
    } else {
        video.pause();
        playbtn.setAttribute("src", "/Images/contactUs/play.png");

    }
}

function stopVideo() {
    video.pause();
    video.currentTime = 0;
    playbtn.setAttribute("src", "/Images/contactUs/play.png");


}

function dragSlider() {

    video.currentTime = video.duration * (slider.value / 100);

}

function pauseWhenDraging() {
    video.pause();
}

function playWhenDragStopsdrag() {
    video.play();
}


function moveSlider() {
    slider.value = video.currentTime / video.duration * 100;

}




