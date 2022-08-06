function updateSettings() {
    $.ajax({
        type: "POST",
        url: "/Setting/Update",
        data: $("form").serialize(),
        headers: {
            RequestVerificationToken: $(
                'input[name="__RequestVerificationToken"]'
            ).val(),
        },
        beforeSend: () => {
            swal.fire({
                title: "please wait..",
                timerProgressBar: true,
                allowEscapeKey: false,
                allowOutsideClick: false,
                showCloseButton: false,
                showConfirmButton: false,
                didOpen: () => swal.showLoading()
            })
        },
        success: result => {
            swal.fire({
                title: "Successfully Saved",
                icon: "success"
            });
        }
    })
}