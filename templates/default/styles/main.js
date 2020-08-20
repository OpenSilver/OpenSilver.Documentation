// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See LICENSE file in the project root for full license information.
//NAVIGATION MORE BTN
var header = document.getElementById("header");
var navMore = document.getElementById("nav_more");
var closeNav = document.getElementById("close_nav");
navMore.onclick = function() {
    navMore.classList.toggle("active");
    header.classList.toggle("active");
};
closeNav.onclick = function() {
    navMore.classList.toggle("active");
    header.classList.toggle("active");
};

//HAMBURGER MENU
var hambMenu = document.getElementById("hamb_menu");
var header = document.getElementById("header");
var root = document.getElementsByTagName('html')[0];
var body = document.body;
hambMenu.onclick = function() {
    hambMenu.classList.toggle("resp_menu-opened");
    header.classList.toggle("active");
    root.classList.toggle('scroll_off');
    body.classList.toggle('scroll_off');
};
