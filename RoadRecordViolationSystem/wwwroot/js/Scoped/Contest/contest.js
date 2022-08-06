function viweOrCrImage(path) {
    $.ajax({
        type: "GET",
        url: "/Contest/ViewOrCrImagePartial",
        data: path,
        success: result => {
            $("#modal-holder").html("").html(result).find(".modal").modal("show");
        }
    });
}


function viewVideoProof(path) {
    console.log(path)
    if (path.path === '') {
        swal.fire({
            icon: "error",
            text: "Complainant did not submit video as proof",
            title:"No video"
        });
    }
    else {
        $.ajax({
            type: "GET",
            url: "/Contest/ProofOfVideoPartial",
            data: path,
            success: result => {
                $("#modal-holder").html("").html(result).find(".modal").modal("show");
            }
        });
    }
}

function showModal(url,model) {
    $.ajax({
        type: "GET",
        url:url,
        data: model,
        success: partialView => {
            $("#modal-holder").html("").html(partialView).find(".modal").modal("show");
        }
    })
}

function validateLettersOnly(el) {
    const symbols = ['!', '@', '#', '~', '`', '$', '%', '^', '&', '*', '(', ')', '_', '-', '+', '=', ':', ';', '"', '\'', '\\', '|', '[', ']', '{', '}', '<', '>', ',', '.', '/', '?'];

    if ((!isNaN(el.value[el.value.length - 1]) || symbols.includes(el.value[el.value.length - 1])) && el.value[el.value.length - 1] !== ' ')
        el.value = el.value.slice(0, el.value.length - 1);
}


function approveContest() {

    if ($("#mediator").val() === "") {
        swal.fire({
            icon: "error",
            title: "Invalid",
            text: "Mediator name is required"
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Contest/ApproveApplication",
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
            headers: {
                RequestVerificationToken: $(
                    'input[name="__RequestVerificationToken"]'
                ).val(),
            },
            data: $("#contest-form").serialize(),
            success: result => {
                swal.fire({
                    icon: "success",
                    title: "Approved!",
                    text: "Successfully set schedule of the contest"
                });
                document.querySelector("#contest-form").reset();
                $("#modal-contest").modal("hide");

                $(".table-responsive-sm").html("").html(result);
                setDataTable();

            }
        })
    }
}

function rejectContest(model) {
    model.reason = $("#reason-input").val();
    $.ajax({
        type: "POST",
        url: "/Contest/RejectContestApplication",
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
        success: result => {
            $("#modal-contest").modal("hide");
            swal.fire({
                icon: "success",
                title: "Rejected",
                text: "Application Rejected"
            });

            $(".table-responsive-sm").html("").html(result);
            setDataTable();
        }
    })
}

$("#modal-holder").on("hidden.bs.modal", function () {
    $("#modal-holder").html("");
})

