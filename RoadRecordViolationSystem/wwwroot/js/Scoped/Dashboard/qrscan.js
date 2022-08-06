"user strict"




const web = document.getElementById("webcam");
const canvas = document.querySelector("canvas");
const cam = new Webcam(web, "user", canvas, null);
const selectElement = document.querySelector("#select");
const signaturePadCanvas = document.getElementById("signature-pad");
const signaturePad = new SignaturePad(signaturePadCanvas);
const pesoFormat = new Intl.NumberFormat("en-IN", { style: 'currency', currency: 'PHP' });
var violationList = [];
var licenseDetails = {};
var violator = {};
var hasNoLicense = false;
var mainContainerCopy = $("#main-container").html();
var accidentInvolves = [];
var coordinates = [];
var officerInvolves = [];
var violatorInfoViaQr = {};
const htmlQr = new Html5Qrcode("qr-reader");
var previousMotoristDTO = {};

localStorage.setItem("url", "https://localhost:44354/api/");
//localStorage.setItem("url", "https://dpossystem.somee.com/api/");



function openCamera() {


    //for testing scanning



        $(".camera-option").removeAttr("hidden");
        $("#webcam").removeAttr("hidden");
        $("#no-license-btn").attr("hidden", true);
        $("#accident-btn").attr("hidden", true);
        $("#capture-btn").removeAttr("hidden");
        $("#open-cam-btn").attr("hidden", true);
        $("#close-cam-btn").removeAttr("hidden");
        $("#qr-cam-btn").attr("hidden", true);
        cam.start();
    


    // for testing violation assigner 
    /*
          $("#modal-popup").modal("show");
       getViolationTypes();*/

}

function switchCamera() {
    cam.flip();
    cam.start();
}




function capture() {

    $.ajax({
        type: "POST",
            url: localStorage.getItem("url") + "EnforcerScanning/send-image",
        contentType: "application/x-www-form-urlencoded",
        beforeSend: () => {
            showSpinner(true);
        },
        dataType: "JSON",
        data: { "imageBase64": cam.snap() },
        success: (result, textStatus, xhr) => {
            if (xhr.status == 200 || result.details !== undefined) {
                console.log(result)
                showSpinner(false);
                $("#modal-popup").modal("show");
                getViolationTypes();
                setLicenseInformation(result, $(".details-holder"));
                setCurrentAddress("#violation-place", "#violation-map");
            }
            else {
                console.log(result)
                showSpinner(false);
                resetPage();
                swal.fire({
                    title: "Unable to scan",
                    text: result.error.message,
                    icon: "error"
                })
            }
        },
        error: err => {
            console.log(err)
            showSpinner(false);
            swal.fire({
                title: "Unable to scan",
                text: err.responseText,
                icon: "error"
            })
        }
    })

}

function setLicenseInformation(scannedDetails,inputFields) {

    const details = [
        "firstName", "lastName", "middleName", "sex", "dob", "weight",
        "height", "nationality_iso3", "address1", "address2", "eyeColor", "expiry", "daysToExpiry", "documentNumber"
    ]

    for (let i = 0; i < inputFields.length; i++) {
        if (scannedDetails.result[details[i]] === undefined) {
            scannedDetails.result[details[i]] = "";
        }
        inputFields[i].innerHTML = setGender(scannedDetails.result[details[i]]);
    }
    
    licenseDetails = {
        GivenName: scannedDetails.result["firstName"],
        LastName: scannedDetails.result["lastName"],
        MiddleName: scannedDetails.result["middleName"],
        Gender: scannedDetails.result["sex"],
        Weight: scannedDetails.result["weight"],
        Height: scannedDetails.result["height"],
        Nationality: scannedDetails.result["nationality_iso3"],
        Address: scannedDetails.result["address1"],
        City: scannedDetails.result["address2"],
        EyeColor: scannedDetails.result["eyeColor"],
        ExpirationDate: formatDateByDash(scannedDetails.result["expiry"]),
        LicenseNo: scannedDetails.result["documentNumber"],
        DateOfBirth: scannedDetails.result["dob"]
    };


    $("#violator-name").html(`${licenseDetails.LastName}, ${licenseDetails.GivenName} ${licenseDetails.MiddleName} `.toUpperCase());
    $("#violator-preview").html(`${licenseDetails.LastName}, ${licenseDetails.GivenName} ${licenseDetails.MiddleName} `.toUpperCase());
    $("#violator-license").html(licenseDetails.LicenseNo);
    $("#violator-license-preview").html(licenseDetails.LicenseNo);
    $("#nl-given-name").val(licenseDetails.GivenName);
    $("#nl-middle-name").val(licenseDetails.MiddleName);
    $("#nl-last-name").val(licenseDetails.LastName);
    $("#nl-age").val(getAge(scannedDetails.result["dob"]));
    $("#nl-gender :selected").text(setGender(licenseDetails.Gender) === "" ? "Male" : setGender(licenseDetails.Gender));
    $("#nl-address").val(licenseDetails.Address + " " + licenseDetails.City);
    document.querySelector("#nl-dob").valueAsDate = formatDate(scannedDetails.result["dob"]);

}

function formatDate(dateString) {
    let date = dateString.toString().split("/");
    return new Date(Date.parse(`${date[0]}-${date[1]}-${date[2]}`));
}

function formatDateByDash(dateString) {
    let date = dateString.toString().split("/")
    return `${date[0]}-${date[1]}-${date[2]}`;
}


function setViolatorInfo() {

  
        violator.GivenName = $("#nl-given-name").val().trim();
        violator.LastName = $("#nl-last-name").val().trim();
        violator.MiddleName = $("#nl-middle-name").val().trim();
        violator.DOB = $("#nl-dob").val().trim();
        violator.Age = $("#nl-age").val().trim();
        violator.Gender = $("#nl-gender").val().trim();
        violator.PhoneNo = $("#nl-phone-no").val().trim();
        violator.Email = $("#nl-email").val().trim();
        violator.Address = $("#nl-address").val().trim();
        violator.DOB = $("#nl-dob").val().trim();
    


}

function setGender(detail) {
    if (detail == "M") {
        return "Male";
    }
    else if (detail == "F") {
        return "Female";
    }
    else {
        return detail;
    }
}



function getViolationTypes() {

   $.ajax({
       url: localStorage.getItem("url") +"EnforcerScanning/violation-types",
       type: "GET",
       contentType: "application/json",
       success: (result, statusText, xhr) => {

           if (xhr.status == 200) {
               let selectBox = document.getElementById("violation-types-selectbox");
               for (let i of result) {
                       let option = document.createElement("option");
                       option.value = i.Id;
                       option.innerHTML = `${i.Code} | ${i.Name} - ₱ ${(i.Fee)}`;
                       selectBox.appendChild(option);
                   
               }

               if (hasNoLicense) {
                   let type = "";
                   let amount = 0;
                   let vioVal = 0;
                   let code = "";

                   $("#violation-types-selectbox option").each(function () {
                       let vioType = $(this).text().slice($(this).text().indexOf("|") + 2, $(this).text().indexOf("-")).trim();
                       if (vioType === "DRIVING WITHOUT LICENSE") {
                           type = vioType;
                           amount = parseFloat($(this).text().slice($(this).text().indexOf("₱") + 1).trim());
                           vioVal = $(this).val();
                           code = $(this).text().slice(0, $(this).text().indexOf("|")).trim();
                       }
                   })

                   violationList.push({ Type: type, Cost: amount,Code:code });
                   $("#violation-types-selectbox option[value='" + vioVal + "']").remove();
               }

               loadViolationTable();
               $("#violation-types-selectbox").select2({
                   dropdownParent: $("#modal-popup")
               });
           }
        }
    });
}




document.getElementById("no-license-form").addEventListener("submit", function (e) {


    e.preventDefault();
    if ($("#nl-age").val() < 18) {
        swal.fire({
            title: "Invalid Age",
            text: "Age doesnt seems not accurate",
            icon: "error"
        })
    }
    else if (validatePhoneNum($("#nl-phone-no").val())) {
        openPage("vehicle-information-page", "no-license-page");    
    }
    else {
        swal.fire({
            title: "Invalid Phone Number",
            text: "Please make sure that you have a correct phone number format",
            icon: "error"
        })
    }
   

})

document.getElementById("vehicle-information-form").addEventListener("submit", function (e) {
    e.preventDefault();
    openPage('violation-assigner-page', 'vehicle-information-page');
    $("#violator-name").html(`${$("#nl-given-name").val()}, ${$("#nl-middle-name").val()} ${$("#nl-last-name").val()} `.toUpperCase());
    $("#violator-license").html(hasNoLicense ? "No License" : licenseDetails.LicenseNo);
});

document.getElementById("accident-form").addEventListener("submit", function (e) {
    e.preventDefault();
    saveAccident();
})

function openPage(pageToOpen, pageToClose) {

    $("." + pageToOpen).removeAttr("hidden");
        $("." + pageToClose).attr("hidden", true);



}

$("#no-license-btn").on("click", () => {
    hasNoLicense = true;
    getViolationTypes();
})

function openPageWithModal(pageToOpen, pageToClose, modalId) {

    $("#"+modalId).modal("show");
    $("." + pageToOpen).removeAttr("hidden");
    $("." + pageToClose).attr("hidden", true);

    $("#btn-back").attr("hidden", true);

    if (pageToOpen === "no-license-page") {
        setCurrentAddress("#violation-place", "#violation-map");
    }
}


function resetPage() {
    
    $(".license-information-page").removeAttr("hidden");
    $(".violation-assigner-page").attr("hidden", true);
    $(".vehicle-information-page").attr("hidden", true);
    $(".no-license-page").attr("hidden", true);
    $(".camera-option").attr("hidden", true);
    $(".accident-page").attr("hidden", true);
    $("#scanned-license-btn").html("Next");
    violationTotalAmount = 0;
    $("#total-amount").html(0);
    $("tbody").html("");
    $("#violation-types-selectbox").empty();
    licenseDetails = {};
    violator = {};
    $("#btn-back").removeAttr("hidden");
    violationList = [];
    $("#webcam").attr("hidden", true);
    $("#no-license-btn").removeAttr("hidden");
    $("#accident-btn").removeAttr("hidden");
    $("#close-cam-btn").attr("hidden", true);
    hasNoLicense = false;

    $("#nl-given-name").removeAttr("readonly");
    $("#nl-middle-name").removeAttr("readonly");
    $("#nl-last-name").removeAttr("readonly");
    $("#nl-gender").removeAttr("disabled");
    $("#nl-address").val(licenseDetails.Address + " " + licenseDetails.City).removeAttr("readonly");
    $("#nl-dob").removeAttr("readonly");
    $("#capture-btn").attr("hidden", true);
    $("#open-cam-btn").removeAttr("hidden");
    $("#qr-cam-btn").removeAttr("hidden");
    $(".preview-violation-page").attr("hidden", true);

    accidentInvolves = [];
    resetForms();
    coordinates = [];
    cam.stop();
    htmlQr.stop();
    previousMotoristDTO = {};
}

function resetForms() {

    document.getElementById("no-license-form").reset();
    document.getElementById("vehicle-information-form").reset();
    document.getElementById("accident-form").reset();
}



function addViolationToDriver() {
    let selectBoxVal = $("#violation-types-selectbox :selected").text();

    const vioVal = $("#violation-types-selectbox").val();
    const type = selectBoxVal.slice(selectBoxVal.indexOf("|")+2, selectBoxVal.lastIndexOf("-")).trim();
    const amount = parseFloat(selectBoxVal.slice(selectBoxVal.indexOf("₱") + 1).trim());
    const code = selectBoxVal.slice(0, selectBoxVal.indexOf("|")).trim();

    console.log("violation")
    console.log(vioVal)

    console.log("text value")
    console.log(selectBoxVal)

    console.log("type")
    console.log(type)

    //remove selected violationtype in selectbox
    $("#violation-types-selectbox option[value='"+vioVal+"']").remove();

    const vioObject = {
        Type: type,
        Cost: amount,
        valId: vioVal,
        Code:code
    }

    console.log(vioObject)

    violationList.push(vioObject);
    loadViolationTable();

    if ( $("#violation-types-selectbox :selected").text() === "") {
        $(".add-violation-btn").attr("hidden", true);
    }

}

function loadViolationTable() {
    let totalAmount = 0;
    $("#violation-list").empty();
    $("#violation-list-preview").empty();

    if (hasNoLicense) {
        $("#violation-list").append(`<tr><td>${violationList[0].Type}</td><td>${pesoFormat.format(violationList[0].Cost)}</td></tr>`);
        $("#violation-list-preview").append(`<tr><td>${violationList[0].Type}</td><td>${pesoFormat.format(violationList[0].Cost)}</td></tr>`);
        totalAmount += violationList[0].Cost;
    }
    else {
        $("#violation-types-selectbox option").each(function () {
            if ($(this).text().slice($(this).text().indexOf("|")+2).toLocaleLowerCase().trim().replace(/ /g, "").includes("drivingwithoutlicense")) {
                $("#violation-types-selectbox option[value='" + $(this).val() + "']").remove();
                return false;
            }
        })
    }


        for (let i of violationList) {
            if (i.Type.toLowerCase().trim().replace(/ /g, "") !== "drivingwithoutlicense") {
                $("#violation-list").append(`<tr><td>${i.Type}</td><td>${pesoFormat.format(i.Cost)}</td><td><a style="cursor:pointer;" onclick="removeViolationToDriver(${i.valId})" ><i class="bi-x"></i></a></td></tr>`);
                $("#violation-list-preview").append(`<tr><td>${i.Type}</td><td>${pesoFormat.format(i.Cost)}</td></tr>`);
                totalAmount += i.Cost;
            }
        }
    
    $("#total-amount").html(pesoFormat.format(totalAmount));
    $("#total-amount-preview").html(pesoFormat.format(totalAmount));
   

}

function removeViolationToDriver(index) {
    let violation = violationList.filter(i => i.valId == index)[0];
    console.log(violation)
    //Create option element
    let option = document.createElement("option");
    option.value = violation.valId;
    option.innerHTML = `${violation.Code} | ${violation.Type} - ₱ ${parseInt(violation.Cost)}`;

    //add created option element to selectbox
    document.querySelector("#violation-types-selectbox").appendChild(option);
    
    violationList = violationList.filter(i => i.valId != index);

    loadViolationTable();

    if ($("#violation-types-selectbox :selected").text() !== "") {
        $(".add-violation-btn").removeAttr("hidden");
    }
}

function saveViolation() {

    setViolatorInfo();
    if (signaturePad.isEmpty()) {
        Swal.fire({
            title: "Signature is missing",
            text: "Please sign the violator signature",
            icon: "error"
        });
    }

    else if (violationList.length == 0) {
        Swal.fire({
            title: "Violation is missing",
            text: "Please Assign A violation before saving",
            icon: "error"
        });
    }
    else {
        let model = {
            "Violations": violationList,
            "LicenseDetails": licenseDetails,
            "vehicleInformation": {
                Model: $(".Model :selected").text(), Type: $(".VehicleType :selected").text(), PlateNo: $(".PlateNo").val(),
                StickerNo: $(".sticker-no").val(), PlaceOfViolation: $("#violation-place").val(),
                Latitude: coordinates[0],
                Longtitude: coordinates[1]
            },
            "Violator" :violator
        };
        // ajax request for saving image signature
        $.ajax({
            url: localStorage.getItem("url") + "EnforcerScanning/save-signature",
            type: "POST",
            ccontentType: "application/x-www-form-urlencoded",
            beforeSend: () => {

                swal.fire({
                    title: "Saving please wait..",
                    timerProgressBar: true,
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    showCloseButton: false,
                    showConfirmButton: false,
                    didOpen: () => swal.showLoading()
                })
            },

            /* in this data the ticketinformationId argument is used to check if signature of the violator exist in the system
             * if exist , then the ticketInformationId should not be null or empty, which means that we will update his/her signature
             * if doesnt, then we will save a signature
             */
            data: { imageBase64: signaturePad.toDataURL(), ticketInformationId : previousMotoristDTO.TicketInformation.Id },
            success: signaturePath => {
                // ajax request for saving violations
                if (previousMotoristDTO === {} || previousMotoristDTO === null) {
                    $.ajax({
                        url: localStorage.getItem("url") + "EnforcerScanning/save-violation",
                        type: "POST",
                        data: model,
                        success: (result, textStatus, xhr) => {
                            if (xhr.status == 200) {
                                swal.close();
                                swal.fire({
                                    icon: "success",
                                    title: "Successfully saved",
                                    timer: 1000
                                });
                                console.log(result)
                                coordinates = [];
                                // ajax request for receipt

                                $.ajax({
                                    type: "POST",
                                    url: "/Receipt/Index",
                                    data: result,
                                    success: (path, textStatus, xhr) => {
                                        if (xhr.status == 200) {

                                            $("#modal-popup").modal("hide");
                                            $("#main-container").html(path);
                                            hasNoLicense = false;
                                        }


                                    }
                                })
                            }
                        },
                        error: (result) => {
                            console.log(result)
                            swal.fire({
                                icon: "error",
                                title: "Cannot save",
                                text: "please try again later"
                            });


                        }
                    })
                }
                else {
                    violationList.forEach(i => i.TicketId = previousMotoristDTO.TicketInformation.Id);

                    $.ajax({
                        url: localStorage.getItem("url") + "EnforcerScanning/save-violation-list",
                        type: "POST",
                        data: { violations: violationList },
                        success: (result, textStatus, xhr) => {
                            if (xhr.status == 200) {
                                swal.close();
                                swal.fire({
                                    icon: "success",
                                    title: "Successfully saved",
                                    timer: 1000
                                });
                                // ajax request for receipt
                                violationList.forEach(violation => previousMotoristDTO.Violations.push(violation))
                                previousMotoristDTO.TicketInformation.ViolatorSignatureImagePath = signaturePath;
                                $.ajax({
                                    type: "POST",
                                    url: "/Receipt/Index",
                                    data: previousMotoristDTO,
                                    success: (path, textStatus, xhr) => {
                                        if (xhr.status == 200) {

                                            $("#modal-popup").modal("hide");
                                            $("#main-container").html(path);
                                            hasNoLicense = false;
                                        }


                                    }
                                })
                            }
                        },
                        error: (result) => {
                            console.log(result)
                            swal.fire({
                                icon: "error",
                                title: "Cannot save",
                                text: "please try again later"
                            });


                        }
                    })
                 
                       
            }
            }
            })
        
    }
}


function showSpinner(flag){

    const mainView = $("#enforcer-main-view");
    const spinner = $(".sk-cube-grid");
    const spinTitle = $("#sk-title");



    if (flag) {
        mainView.attr("hidden",true);
        spinner.removeAttr("hidden");
        spinTitle.removeAttr("hidden")
    }
    else {
        mainView.removeAttr("hidden");
        spinner.attr("hidden", true);
        spinTitle.attr("hidden", true);
    }
}

function saveAccident() {


    if (accidentInvolves.length > 0) {

        let accidentDetails = {};

        accidentDetails.Description = $("#ac-desc").val().trim();
        accidentDetails.Place = $("#ac-place").val().trim();
        accidentDetails.Latitude = coordinates[0];
        accidentDetails.Longtitude = coordinates[1];

        var formData = new FormData();
        formData.append("image", document.querySelector(".accident-image-file").files[0]);

        var model = {
            "Involves": accidentInvolves,
            "Accident": accidentDetails,
            "OfficerInvolves": officerInvolves
        };

        $.ajax({
            url: localStorage.getItem("url") + "EnforcerScanning/save-accident-image",
            type: "POST",
            contentType: false,
            processData: false,
            data: formData,
            beforeSend: () => {

                swal.fire({
                    title: "Saving please wait..",
                    timerProgressBar: true,
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    showCloseButton: false,
                    showConfirmButton: false,
                    didOpen: () => swal.showLoading()
                })
            },
            success: (result, statusText, xhr) => {
                console.log("success")
                console.log(xhr.status)
                if (xhr.status == 200) {

                    $.ajax({
                        url: localStorage.getItem("url") + "EnforcerScanning/add-accident",
                        type: "POST",
                        data: model,
                        success: result => {
                            swal.fire({
                                title: "Success",
                                text: "Succesfully save accident",
                                icon: "success",
                            })

                            $("#modal-popup").modal("hide");
                            coordinates = [];
                        }
                    })

                }
                else {
                    swal.fire({
                        icon: "error",
                        title: "Cannot save",
                        text: result.responseText
                    });
                }
            },
            error: (result) => {
                console.log("error ")
                console.log(result)
                swal.fire({
                    icon: "error",
                    title: "Cannot save",
                    text: "please try again later"
                });
            }

        })

        console.log(accidentDetails);
    }
    else {
        swal.fire({
            title: "Cannot proceed",
            icon: "error",
            text: "Please add involves to the accident"
        });
    }
    
}

//reset page when modal is closed

$("#modal-popup").on("hidden.bs.modal", function () {
    resetPage();
});




function setMaxLengthNum(maxlength, el) {
    el.value = el.value.replace(/[e\+\-]/gi,"");
    if (el.value.length >= maxlength) {
        el.value = el.value.slice(0, maxlength);
    }

}

function validatePhoneNum(el) {
    if (el === null || el === "")
        return true;
    else if (el.length != 11 || el.slice(0, 2).toString() !== "09") {
        return false;
    }
    return true;
}

function validateLettersOnly(el) {
    const symbols = ['!','@','#','~','`','$','%','^','&','*','(',')','_','-','+','=',':',';','"','\'','\\','|','[',']','{','}','<','>',',','.','/','?'];

    if ((!isNaN(el.value[el.value.length - 1]) || symbols.includes(el.value[el.value.length - 1]) ) && el.value[el.value.length - 1] !== ' ')
        el.value = el.value.slice(0, el.value.length - 1);
}



$(".btn-outline-dark").on("click", function () {
    $("#modal-popup").modal("hide");
});


function setCurrentAddress(field,mapImg) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(position => {
            coordinates[0] = position.coords.latitude;
            coordinates[1] = position.coords.longitude;
            const locationIqAPI = {
                "async": true,
                "crossDomain": true,
                "url": "https://us1.locationiq.com/v1/reverse.php?key=pk.d45754a81490e6f0b9d7258d6c5b6393&lat=" + position.coords.latitude + "&lon=" + position.coords.longitude + "&format=json",
                "method":"GET"
            }

            $.ajax(locationIqAPI).done(Response => {
                console.log(Response.display_name)
                $(field).val(Response.display_name).attr("readonly", true);
                $(mapImg).removeAttr("hidden");
            });
        });
    }
    else {
        $("#accident-map").attr("hidden",true);
        swal.fire({
            icon: "warning",
            title: "Place of accident",
            text:'Automatic set of place of accident is not supported in your browser, please set place of accident manually'
        })
    }
}

$("#accident-btn").on("click", function () {
    setCurrentAddress("#ac-place","#accident-map");
});


function addInvolveToAccident() {
    let hasEmpty = false;

    let involves = {};
    involves.GivenName = $("#ac-given-name").val().trim();
    involves.LastName = $("#ac-last-name").val().trim();
    involves.MiddleName = $("#ac-middle-name").val().trim();
    involves.DOB = $("#ac-dob").val().trim();
    involves.Age = $("#ac-age").val().trim();
    involves.Gender = $("#ac-gender").val().trim();
    involves.EmergencyContactNo = $("#ac-contact-no").val().trim();
    involves.Email = $("#ac-email").val().trim();
    involves.Address = $("#ac-address").val().trim();

    for (let i in involves) {
        console.log(involves[i])
        console.log(isFieldEmpty(involves[i]))
        if (isFieldEmpty(involves[i]) && i.toString() !== "EmergencyContactNo" && i.toString() !=="Email") {
            swal.fire({
                title: "Empty field",
                icon: "error",
                text: "field " + i + " is required"
            });
            hasEmpty = true;
            break;
        }
    }

    if (!hasEmpty) {
        if (validatePhoneNum($("#ac-contact-no").val())) {
            /*
            accidentDetails.Description = $("#ac-desc").val().trim();
            accidentDetails.Place = $("#ac-place").val().trim();
            */

            accidentInvolves.push(involves);
            refreshAccidentList();
            console.log(accidentInvolves);
            resetAccidentInvolvesInput();
        }
        else {
            swal.fire({
                title: "Invalid Phone Number",
                text: "Please make sure that you have a correct phone number format",
                icon: "error"
            })
        }
    }
}

function removeInvolves(i) {
    console.log(i);
    accidentInvolves = accidentInvolves.filter((item, index) => index != i);
    console.log(accidentInvolves);
    refreshAccidentList();
}

function isFieldEmpty(field) {
    return field === "" || field === undefined || field === null;
}

function refreshAccidentList() {
    let index = 0;
    $(".accident-table-list").empty();
    for (let i of accidentInvolves) {
        $(".accident-table-list").append(`<tr><td>${i.LastName},${i.GivenName} ${i.MiddleName}</td><td>${i.EmergencyContactNo}</td><td>${i.Email}</td>
    <td><a style="cursor:pointer;" onclick="removeInvolves(${index})" ><i class="bi-x"></i></a></td></tr>`);
        index++;
    }
}

function addOfficerInvolve() {
    let hasEmpty = false;
    const officerInvolve = {
        GivenName:$("#police-given-name").val().trim(),
        MiddleName: $("#police-middle-name").val().trim(),
        LastName: $("#police-last-name").val().trim(),
        Gender: $("#police-gender").val(),
        Age:$("#police-age").val()
    }

    for (let i in officerInvolve) {
        if (isFieldEmpty(officerInvolve[i])) {
            swal.fire({
                title: "Empty field",
                icon: "error",
                text: "field " + i + " is required"
            });
            hasEmpty = true;
            break;
        }
    }

    if (!hasEmpty)
        officerInvolves.push(officerInvolve);

    refreshOfficerList();
}

function refreshOfficerList() {
    let index = 0;
    $(".officer-table-list").empty();
    for (let i of officerInvolves) {
        $(".officer-table-list").append(`<tr><td>${i.LastName}, ${i.GivenName} ${i.MiddleName}</td><td>${i.Gender}</td><td>${i.Age}</td>
    <td><a style="cursor:pointer;" onclick="removeOfficerInvolves(${index})" ><i class="bi-x"></i></a></td></tr></tr>`)
    }
}

function removeOfficerInvolves(i) {
    console.log(i);
    officerInvolves = officerInvolves.filter((item, index) => index != i);
    refreshOfficerList();
}

function resetAccidentInvolvesInput() {
    $("#ac-given-name").val("");
    $("#ac-last-name").val("");
    $("#ac-middle-name").val("");
    $("#ac-dob").val("");
    $("#ac-age").val("");
    $("#ac-gender").val("");
    $("#ac-contact-no").val("");
    $("#ac-email").val("");
    $("#ac-address").val("");
}

function resetOfficerInvolvesInput() {
    $("police-given-name").val("");
    $("police-middle-name").val("");
    $("police-last-name").val("");
    $("police-gender").val("");
}

function getAge(dateString) {
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}


$("#police-chkbx").change(function (){
    if (this.checked) 
        $("#police-info").removeAttr("hidden");
    else
        $("#police-info").attr("hidden", true);

    officerInvolves = [];
    refreshOfficerList();
})

const qrCodeSuccessCallback = (decodedText, decodedResult) => {

    $.ajax({
        type: "GET",
        url: localStorage.getItem("url") + "EnforcerScanning/get-info?hash=" + decodedText,
        success: (dto) => {

            $(".modal").modal("show");
            hasNoLicense = false;
            getViolationTypes();
            openPage("violation-assigner-page", "license-information-page");
            $("#violator-name").html(`${dto.Violator.GivenName}, ${dto.Violator.MiddleName} ${dto.Violator.LastName} `.toUpperCase());
            $("#violator-license").html(dto.LicenseDetails === null ? "No License" : dto.LicenseDetails.LicenseNo);
            previousMotoristDTO = dto;

            loadPreviousViolations(dto);
        },
        error: response => {
            console.log(response)
            if (response.status == 400) {
                swal.fire({
                    icon: "error",
                    title: "Unable to scan",
                    text: response.responseText
                });
            }
            else {
                swal.fire({
                    icon: "error",
                    title: "Unable to scan",
                    text: `Error Code: ${response.status} please contact administrator`
                });
            }
        }
    })

}

function scanQr() {

    $("#no-license-btn").attr("hidden", true);
    $("#accident-btn").attr("hidden", true);
    $("#open-cam-btn").attr("hidden", true);
    $("#close-cam-btn").removeAttr("hidden");
    $("#qr-cam-btn").attr("hidden", true);

    const config = { fps: 10, qrbox: { width: 250, height: 250 } };
    
    htmlQr.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);
}

function loadPreviousViolations(dto) {
    $(".previous").removeAttr("hidden");
    let table = $("#previous-violation-list-preview").empty();
    let previousTotalAmount = 0;

    for (let violation of dto.Violations) {
        table.append(`<tr><td>${violation.Type}</td><td>${pesoFormat.format(violation.Cost)}</td></tr>`);
        previousTotalAmount += violation.Cost;
    }

    $("#previous-total-amount-preview").html(pesoFormat.format(previousTotalAmount));
}













