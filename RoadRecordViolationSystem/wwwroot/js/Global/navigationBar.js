const btn = document.querySelector(".btn-hide");

const tempNavbar = document.querySelector(".temp-sidebar");
const navBar = document.querySelector(".sidebar");
const header = document.querySelector(".sidebar-header");
const mainContainer = document.querySelector(".site-content");
let isScreenSizeSmall = false;

btn.addEventListener("click", resizeTempNavbar);
window.onresize = toggleSmallScreenNavbar;
window.onload = toggleSmallScreenNavbar;

function resizeTempNavbar() {
  if (navBar.clientWidth == 250) {
    toggleSiteContent("50px");
    toggleNavbar("", "35px");
  } else if (isScreenSizeSmall) {
    navBar.style.width = "250px";
    header.innerHTML = "DPOS Violation System";
    tempNavbar.style.width = "35px";
  } else if (tempNavbar.clientWidth != 250) {
    toggleSiteContent("250px");
    toggleNavbar("DPOS Violation System", "250px");
  }
}

function toggleSiteContent(paddingLeftVal) {
  const siteContent = document.querySelector(".site-content");

  if (siteContent !== null) {
    siteContent.style.paddingLeft = paddingLeftVal;
  }
}

function toggleSmallScreenNavbar() {
  if (window.innerWidth <= 683) {
    toggleSiteContent("50px");
    toggleNavbar("", "35px");
    isScreenSizeSmall = true;
  } else {
    isScreenSizeSmall = false;
  }
}

function toggleNavbar(textHeader, navbarWidth) {
  navBar.style.width = tempNavbar.style.width = navbarWidth;
  header.innerHTML = textHeader;
}
