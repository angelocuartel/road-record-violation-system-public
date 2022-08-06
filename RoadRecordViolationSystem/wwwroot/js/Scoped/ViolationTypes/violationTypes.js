
function addRequestAjax() {
    if ($("#code").val() === "") {
        Swal.fire({
            title: "Invalid ",
            icon: "error",
            text: "Code should not be empty"
        })
    }
    else {
        if ($("form").valid()) {
            $.ajax({
                url: "/ViolationTypes/Add",
                type: "POST",
                data: { Name: $("#name").val(), Fee: $("#fee").val(), Description: $("#desc").val(), Code: $("#code").val(), Penalty: $("#penalty").val() },
                headers: {
                    RequestVerificationToken: $(
                        'input[name="__RequestVerificationToken"]'
                    ).val(),
                },
                success: view => {

                    if (!view.invalidModel) {
                        $(".table-responsive-sm").html(view);

                        setDataTable();

                        swalToast.fire({
                            title: "Successfully Add",
                            icon: "success"
                        });

                        removeInput();
                        removeValidInput();
                    }

                    else {
                        Swal.fire({
                            title: "Invalid Name",
                            icon: "error",
                            text: view.errors
                        })

                        removeValidInput();
                    }

                    
                }

            });
        }
    }
}

$("#fee").change(function (e) {
    e.preventDefault();

    if (!$("#fee").val().includes("."))
        $("#fee").val(parseInt($("#fee").val()).toFixed(2));


});




function removeValidInput() {
    $("#name").removeClass("is-valid");
    $("#fee").removeClass("is-valid");
    $("#desc").removeClass("is-valid");
    $("#code").removeClass("is-valid");
    $("#penalty").removeClass("is-valid");
}

function removeInput() {
    $("#name").val("");
    $("#fee").val(null);
    $("#desc").val("");
    $("#code").val("");
    $("#penalty").val("");
}