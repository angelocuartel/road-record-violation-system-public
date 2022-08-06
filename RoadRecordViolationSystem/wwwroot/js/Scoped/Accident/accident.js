function showViewMoreModal(model) {
    $.ajax({
        type: "GET",
        url: "/Accident/ViewAccidentPartial",
        data: model,
        success: partial => {
            $("#view-more-modal-holder").html("").html(partial);
            $("#view-more-modal-holder").find(".modal").modal("show");
        }
    })
}