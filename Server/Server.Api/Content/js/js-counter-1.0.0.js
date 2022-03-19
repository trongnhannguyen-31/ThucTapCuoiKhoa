/*!
	*
	* js-counter | v1.0.0
	* Created by Jakub Stęplowski
	*
	* !!! jQuery Required
	*
!*/

var cdate=currentDate(),gdate=goalDate(),days=0,hours=0,minutes=0,seconds=0,_onesec=1000,_onemin=_onesec*60,_onehou=_onemin*60,_oneday=_onehou*24,jscount_loaded=false;$(document).ready(function(){gdate=goalDate()});setInterval(function(){cdate=currentDate();remainingTime();loadDefaultHTML()},500);function currentDate(){return new Date()}function goalDate(){var a=$("#js-counter").attr("data-setTime");a=""+a;d=a.split("T");b=d[0].split("-");gyear=b[0];gmonth=b[1];gday=b[2];d[1]=""+d[1];b=d[1].split(":");ghour=b[0];gminutes=b[1];gseconds=b[2];return new Date(gyear,gmonth-1,gday,ghour,gminutes,gseconds,0)}function remainingTime(){var c=cdate.getTime(),a=gdate.getTime(),g=a-c,f=g*1000,e=0;if(a>c){days=Math.floor(g/_oneday);e=g%_oneday;hours=Math.floor(e/_onehou);e=e%_onehou;minutes=Math.floor(e/_onemin);e=e%_onemin;seconds=Math.floor(e/_onesec);if(hours<10){hours="0"+hours}if(minutes<10){minutes="0"+minutes}if(seconds<10){seconds="0"+seconds}}else{days="0";hours="00";minutes="00";seconds="00"}}function loadDefaultHTML(){$("#js-counter-days").text(days);$("#js-counter-hours").text(hours);$("#js-counter-minutes").text(minutes);$("#js-counter-seconds").text(seconds);if(!jscount_loaded){window.scrollTo(0,0);$("#loading").fadeIn();$("#loading").css("visibility","hidden");$("#loading").css("opacity","0");jscount_loaded=true}};