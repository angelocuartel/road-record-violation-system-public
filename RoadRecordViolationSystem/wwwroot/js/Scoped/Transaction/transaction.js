function openTransaction(model) {
    console.log(model)
    $.ajax({
        type: "GET",
        data: model,
        beforeSend: () => {
            swal.fire({
                title: "Loading..",
                text:"Opening transaction please wait",
                timerProgressBar: true,
                allowEscapeKey: false,
                allowOutsideClick: false,
                showCloseButton: false,
                showConfirmButton: false,
                didOpen: () => swal.showLoading()
            })
        },
        url: "/Transaction/TransactionView",
        success: page => {
            swal.close();
            $(".transact-table").html("").html(page);
        }
    })
}


function markAsPaid(model) {

    Swal.fire({
        icon: "question",
        title: "Mark as paid",
        text: "Do you want to mark this transaction as paid ?",
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "Later"
    }).then(result => {

        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                data: model,
                url: "/Transaction/MarkAsPaid",

                success: (page,statusText,xhr) => {
                    if (xhr.status == 200) {
                        swal.fire({
                            icon: "success",
                            title: "Success",
                            text: "Mark as paid successfully"
                        }).then(result => {
                            if (result.isConfirmed) {

                                var printContents = $(".to-be-print").removeAttr("hidden").html();

                                $("body").html("").html(printContents);
                                window.print();

                                window.location.replace("/Transaction/Index");


                            }
                        })


                    }
                }
            })
        }
    });

}

function printReceipt() {
    var printContents = $(".to-be-print").removeAttr("hidden").html();

    $("body").html("").html(printContents);
    window.print();

    window.location.replace("/Transaction/Index");
}