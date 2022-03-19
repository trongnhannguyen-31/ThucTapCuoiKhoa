/*!
	*
	* js-slideshowbg | v1.0.0
	* Created by Jakub Stęplowski
	*
	* !!! jQuery Required
	*
!*/

var currentIndex=0,prevIndex=0;var cdate,pdate=null;function toTimeString(i){var a=1000,j=a*60,d=j*60,c=d*24;var h=Math.floor(i/c),e=i%c,f=Math.floor(e/d),e=e%d,b=Math.floor(e/j),e=e%j,g=Math.floor(e/a);if(f<10){f="0"+f}if(b<10){b="0"+b}if(g<10){g="0"+g}return f+":"+b+":"+g}function activateSlideshow(f,a,e){if(a.length>1){f.append('<div id="slideshow-container" style="position: absolute; width: 0; height: 0; overflow: hidden;"></div>');a.forEach(function(h,g){$("#slideshow-container").append('<img id="slideshow-'+g+'" src="'+h+'" style="visibility: hidden;"/>')});var b=0;var c=function(){$(f).css("background-image","url('"+a[b]+"')")};c();var d=setInterval(function(){(b<a.length-1)?b++:b=0;c()},e*1000)}};
