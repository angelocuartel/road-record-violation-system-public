$(document).ready(() => {
  setDataTable();
});

function setDataTable() {

    let buttonCols = [];
    let headers = document.querySelector("table").rows[0].cells.length;

    for (let i = 0; i < headers - 2; i++)
        buttonCols.push(i);

  $("table").DataTable({
    dom: "Bfrtip",
    language: {
      searchPlaceholder: "Example HelloWorld",
    },
      buttons: [{ extend: "print", title: "Daily report", exportOptions: { columns: buttonCols } },
          { extend: "csvHtml5", title: "Daily report", exportOptions: { columns: buttonCols } },
          { extend: "pdfHtml5", title: "Daily report", exportOptions: { columns: buttonCols } },
          { extend: "excelHtml5", title: "Daily report", exportOptions: { columns: buttonCols } }
      ],
  });
}

const swalToast = Swal.mixin({
  toast: true,
  position: "top-end",
  timer: 2000,
  showConfirmButton: false,
  timerProgressBar: true,
});

function isInputValidInServer() {
  let isValid = "";

  $.ajax({
    type: "POST",
    url: "/AdminAccounts/ValidateModelBeforeUploadImage",
    data: $("form").serialize(),
    async: false,
    success: (jsonResult) => {
      isValid = jsonResult;
    },
    error: (error) => {},
  });

  return isValid;
}

function requestAjaxAddUpdate(urlForm) {
  if ($(".form-ajax").valid()) {
    $.ajax({
      url: urlForm,
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
      type: "POST",
      data: $(".form-ajax").serialize(),
        success: (partialForm) => {
            swal.close();
        const modal = $("#modal-placeholder");

        if (partialForm.IsSuccess) {
          modal.find(".modal").modal("hide");
            modal.find(".modal").html("");

            swalToast.fire({
                icon: "success",
                title: `Successfully ${urlForm.substr(
                    urlForm.lastIndexOf("/") + 1
                )}`,
            });
        } else if (
          partialForm.includes(`table table-bordered table-striped table-hover`)
        ) {
          modal.find(".modal").modal("hide");
          $(".table-responsive-sm").html("").html(partialForm);

            swalToast.fire({
                icon: "success",
                title: `Successfully ${urlForm.substr(
                    urlForm.lastIndexOf("/") + 1
                )}`,
            });

            setDataTable();

        } else if (partialForm.includes(`modal-body`)) {
          const modal = $("#modal-placeholder");
          modal.find("#modal-body-footer-holder").html("");
          modal.find(".modal").modal("show");
          modal.find("#modal-body-footer-holder").html(partialForm);
          $.validator.unobtrusive.parse("form");
        }
      },
      error: (xhr, status, error) => {
        console.log(xhr);
        console.log(status);
        console.log(error);
      },
    });
  }
}

function requestAjaxShowModal(urlForm, title, model) {
  $.ajax({
    url: urlForm,
    type: "GET",
    data: model,
    dataType: "html",
    success: (partialForm) => {
      const modal = $("#modal-placeholder");

      modal.find("#modal-body-footer-holder").html("");
      modal.find(".modal").modal("show");
      modal.find(".modal-title").html(title);
      modal.find("#modal-body-footer-holder").html(partialForm);

      $.validator.unobtrusive.parse("form");
    },
    error: (xhr, status, error) => {
      console.log(xhr);
      console.log(status);
      console.log(error);
    },
  });
}

function requestAjaxDelete(urlForm, param) {

    Swal.fire({
        icon: "warning",
        title: "Confirmation",
        text: "Do you want to proceed?",
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "Change my mind"
    }).then(result => {

        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                url: urlForm,
                data: param,
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
                success: (opResult) => {
                    swal.close();
                    $(".table-responsive-sm").html("").html(opResult);

                    swalToast.fire({
                        icon: "success",
                        title: `Successfully ${urlForm.substr(urlForm.lastIndexOf("/") + 1)}`,
                    });

                    setDataTable();
                },
            });
        }
    });
}

function uploadPicture(urlForm) {
  if ($(".form-ajax").valid()) {
    let result = isInputValidInServer();

    if (result.isValid) {
      var imageFile = $("#image-upload")[0].files[0];

      var formData = new FormData();

      if ($("#profile-holder").val() !== "") {
        formData.append("newImage", imageFile);
        formData.append("oldImagePath", $("#profile-holder").val());
      } else {
        formData.append("Image", imageFile);
      }

      $.ajax({
          type: "POST",

        url:
          $("#profile-holder").val() === ""
            ? "/AdminAccounts/UploadImage"
            : "/AdminAccounts/UpdateImage",
        processData: false,
        contentType: false,
        data: formData,
        success: (jsonResult) => {
          $("#profile-holder").val(jsonResult.imagePath);

          requestAjaxAddUpdate(urlForm);

          swalToast.fire({
            icon: "success",
            title: `Successfully ${urlForm.substr(
              urlForm.lastIndexOf("/") + 1
            )}`,
          });
        },
        error: (error) => {},
      });
    } else {
      Swal.fire({
        icon: "error",
          title: "Oops",
          text:"One of your input is invalid in server please change your profile picture again if you changed it"
      });
      const modal = $("#modal-placeholder");
      //     $("profile-holder").val('');
      modal.find("#modal-body-footer-holder").html("");
      modal.find(".modal").modal("show");
      modal.find("#modal-body-footer-holder").html(result);
      $.validator.unobtrusive.parse("form");
    }
  }
}

var imageFileCopy = null;

function requestAjaxAddUpdateWithFile(url) {
    if ($('.form-ajax').valid()) {
        let imageFile = document.querySelector("#file-holder");
        let model = new FormData($(".form-ajax").get(0));


        if (imageFile.files.length > 0) {
            imageFileCopy = imageFile;
            model.append(imageFile.getAttribute("name"), imageFile.files[0]);
        } else if (imageFileCopy !== null) {
            model.append(imageFileCopy.getAttribute("name"), imageFileCopy.files[0]);
        }

        $.ajax({
            url: url,
            type: "POST",
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
            processData: false,
            contentType: false,
            data: model,
            success: (requestResult) => {
                swal.close();
                const modal = $("#modal-placeholder");
                if (
                    requestResult.includes(`table table-bordered table-striped table-hover`)
                ) {
                    modal.find(".modal").modal("hide");
                    $(".table-responsive-sm").html("").html(requestResult);

                    swalToast.fire({
                        icon: "success",
                        title: `Successfully ${url.substr(url.lastIndexOf("/") + 1)}`,
                    });

                    setDataTable();
                    imageFileCopy = null;
                } else if (requestResult.includes(`modal-body`)) {
                    modal.find("#modal-body-footer-holder").html("");
                    modal.find(".modal").modal("show");
                    modal.find("#modal-body-footer-holder").html(requestResult);
                    $.validator.unobtrusive.parse("form");

                    if (imageFileCopy !== null) {
                        document.querySelector("#pictureOutput").src = URL.createObjectURL(
                            imageFileCopy.files[0]
                        );
                    }
                }
            },
        });
    }
}

$(".modal").on("hidden.bs.modal", () => {
  imageFileCopy = null;
});
