

var cropper;
var croptedImage = null;

function showCropImage() {
  $("#cropping-modal").modal("show");
  document.querySelector("#cropping-image-holder").src = URL.createObjectURL(
    document.querySelector("input[type=file]").files[0]
  );
    cropper = new Cropper(document.querySelector("#cropping-image-holder"), {});
}

$("#cropping-modal").on("hidden.bs.modal", () => {
  cropper.destroy();
});

function setCroppedImage() {
  document.querySelector("#pictureOutput").src = cropper
    .getCroppedCanvas()
    .toDataURL("image/jpeg");
  croptedImage = cropper.getCroppedCanvas();


        let model = new FormData();
        model.append("ProfilePicture", $("#recent-profile").val());
        croptedImage.toBlob(blob => {
            model.append("Picture", blob);
            $.ajax({
                url: "/MyAccount/UpdateProfilePicture",
                type: "POST",
                contentType: false,
                processData: false,
                data: model,
                success: (result, statusText, xhr) => {
                    if (xhr.status == 200) {
                        $("#cropping-modal").modal("hide");
                        cropper.destroy();
                        enableEdit();
                        notif.fire({
                            icon: "success",
                            title: "Successfully Update Profile Picture",
                        });
                    }
                    else {
                        swal.fire({
                            icon: "error",
                            title: "Cannot update profile picture",
                            text: "Oops something went wrong, please try again later"
                        });
                    }
                }
            })
        }, "image/jpg");


}

function enableEdit() {
  if ($("#edit-button").html() == "Edit") {
    const inputs = $("input");

    for (let i = 0; i < inputs.length - 1; i++) {
      inputs[i].removeAttribute("disabled");
      }
      $("#gender").removeAttr("disabled");
    $("#edit-button").html("Update");
  } else {
    updateProfileViaAjax();
  }
}

function resetValidInputs() {
  const inputs = $("input").not("input[type=file]").not(":hidden");

  for (let i = 0; i < inputs.length; i++) {
    inputs.removeClass("is-valid");
  }
}

function updateProfileViaAjax() {
  let formData = new FormData($("form").get(0));
  formData.append("ProfilePicture", $("#profilePicturePath").val());

  if (croptedImage === null) {
    requestAjax(formData);
  } else {
    croptedImage.toBlob((blob) => {
      formData.append("Picture", blob);
      console.log(blob);
      requestAjax(formData);
    }, "image/jpg");
  }
}

function requestAjax(model) {
  $.ajax({
    url: "/MyAccount/UpdateProfile",
    type: "POST",
    contentType: false,
    processData: false,
    data: model,
    success: (jsonObj) => {
      resetValidInputs();

      notif.fire({
        icon: "success",
        title: "Successfully Update Account",
      });
    },
  });
}

const notif = swal.mixin({
  toast: true,
  position: "top",
  timer: 2000,
  showConfirmButton: false,
  timerProgressBar: true,
});

function cancelCrop() {
    cropper.destroy();
    document.querySelector(".preview-profile").src = null;
    $("#crop-image-btn").html("Crop Image");
}

function setCroppedImageToPreview() {
document.querySelector(".preview-profile").src = cropper.getCroppedCanvas()
            .toDataURL("image/jpeg");
}


