
function archiveActiveUser(urlForm, param) {




        $.ajax({
            type: "POST",
            url: urlForm,
            data: param,
            headers: {
                RequestVerificationToken: $(
                    'input[name="__RequestVerificationToken"]'
                ).val(),
            },
            success: (opResult) => {

                $(".table-responsive-sm").html("").html(opResult);

                swalToast.fire({
                    icon: "success",
                    title: `Successfully ${urlForm.substr(urlForm.lastIndexOf("/") + 1)}`,
                });

                setDataTable();
                $("#modal-placeholder").find(".modal").modal("hide");
            },
        });

    
}