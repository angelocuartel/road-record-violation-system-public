window.addEventListener('DOMContentLoaded', event => {

    // Navbar shrink function
    var navbarShrink = function () {
        const navbarCollapsible = document.body.querySelector('#mainNav');
        if (!navbarCollapsible) {
            return;
        }
        if (window.scrollY === 0) {
            navbarCollapsible.classList.remove('navbar-shrink')
        } else {
            navbarCollapsible.classList.add('navbar-shrink')
        
        }

    };

    // Shrink the navbar 
    navbarShrink();

    // Shrink the navbar when page is scrolled
    document.addEventListener('scroll', navbarShrink);

    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            offset: 74,
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

});

// Accodion
var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
  acc[i].addEventListener("click", function() {
    this.classList.toggle("active");
    var panel = this.nextElementSibling;
    if (panel.style.maxHeight) {
      panel.style.maxHeight = null;
    } else {
      panel.style.maxHeight = panel.scrollHeight + "px";
    } 
  });
}




function searchViolation() {
   console.log($("#ticket-input").val());
    $.ajax({
        type: "POST",
        url: "/Violation/GetViolationsByTicketNo",
        data: { ticketNo: $("#ticket-input").val() },
        headers: {
            RequestVerificationToken: $(
                'input[name="__RequestVerificationToken"]'
            ).val()
        },
        success: vioList => {
            $(".violation-table").html("").html(vioList);
        },
        error: result => {

            console.log(result);
            swal.fire({
                title: "No record",
                text: result.responseText+" It looks like you are clear.",
                icon:"success"
                
            })
        }

        });
}

console.log($("#checkViolation").html())


$(document).on('hidden.bs.modal', '#checkViolation', function () {
    $(".violation-table").html("");
    $("#ticket-input").val("");
})
