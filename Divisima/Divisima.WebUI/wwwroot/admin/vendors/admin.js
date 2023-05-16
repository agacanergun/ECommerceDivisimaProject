var silinecekID;
$(".slideDelete").click(function () {
    silinecekID = $(this).attr("rowID");
    $("#modelDelete").modal();
});
$(".brandDelete").click(function () {
    silinecekID = $(this).attr("rowID");
    $("#modelDelete").modal();
});
$(".categoryDelete").click(function () {
    silinecekID = $(this).attr("rowID");
    $("#modelDelete").modal();
});


function deleteSlide() {
    $.ajax({
        type: "POST",
        url: "/admin/slayt/sil",
        data: { id: silinecekID },
        success: function (result) {
            if (result == "OK") location.href = "/admin/slayt";
            else alert(result);
        }
    });
}

function deleteBrand() {
    $.ajax({
        type: "POST",
        url: "/admin/marka/sil",
        data: { id: silinecekID },
        success: function (result) {
            if (result == "OK") location.href = "/admin/marka";
            else alert(result);
        }
    });
}

function deleteCategory() {
    $.ajax({
        type: "POST",
        url: "/admin/kategori/sil",
        data: { id: silinecekID },
        success: function (result) {
            if (result == "OK") location.href = "/admin/kategori";
            else alert(result);
        }
    });
}


