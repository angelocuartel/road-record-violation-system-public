function showMyLogHistoryModal(urlForm, title, model) {
    $.ajax({
        url: urlForm,
        type: "GET",
        data: model,
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
        dataType: "html",
        success: (partialForm) => {
            swal.close();
            const modal = $("#my-loghistory-modal-holder");

            modal.html("");
            modal.html(partialForm);
            modal.find(".modal").modal("show");
            modal.find(".modal-title").html(title);


        },
        error: (xhr, status, error) => {
            console.log(xhr);
            console.log(status);
            console.log(error);
        },
    });
}

function deleteAll(urlForm, param) {
    Swal.fire({
        title: "Warning",
        icon: "warning",
        text: "Are you sure you want to DELETE ALL Log history?",
        showCancelButton:true,
        confirmButtonText: "Yes",
        cancelButtonText: "Change my mind"
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: urlForm,
                type: "POST",
                beforeSend: () => {
                    swal.fire({
                        title: "Deleting please wait..",
                        timerProgressBar: true,
                        allowEscapeKey: false,
                        allowOutsideClick: false,
                        showCloseButton: false,
                        showConfirmButton: false,
                        didOpen: () => swal.showLoading()
                    })
                },
                data: param,
                headers: {
                    RequestVerificationToken: $(
                        'input[name="__RequestVerificationToken"]'
                    ).val(),
                },
                success: (partialForm) => {
                    const modal = $("#my-loghistory-modal-holder");

                    modal.find(".modal").modal("hide");
                    modal.html("");
                    modal.html(partialForm);
                    modal.find(".modal").modal("show");
                    swal.close();
                },
                error: (xhr, status, error) => {
                    console.log(xhr);
                    console.log(status);
                    console.log(error);
                },
            });
        }
    })
   
}

    function deleteLogHistory( param) {
        $.ajax({
            type: "POST",
            url: "/CurrentUserLogHistory/Delete",
            data: { logId: param },
            headers: {
                RequestVerificationToken: $(
                    'input[name="__RequestVerificationToken"]'
                ).val(),
            },
            success: (modalView) => {

                const modal = $("#my-loghistory-modal-holder");

                modal.find(".modal").modal("hide");
                modal.html("");
                modal.html(modalView);
                modal.find(".modal").modal("show");

                const swalToast = Swal.mixin({
                    toast: true,
                    position: "top-end",
                    timer: 2000,
                    showConfirmButton: false,
                    timerProgressBar: true,
                });

                swalToast.fire({
                    icon: "success",
                    title: `Successfully Delete`,
                });

            },
        });
    }
