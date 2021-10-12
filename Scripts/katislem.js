function AltKategoriSec() {

    var katID = $('#akat_id').val();

    $.ajax({
        url: "/araclar/altkategoriler",
        type: "GET",
        dataType: "JSON",
        data: { id: katID },
        success: function (cevap) {

            $("#bkat_id").html("");

            //$("#altKategori").append(
            //    $('<option></option>').val('-1').html('Alt kategori seçiniz'));

            $.each(cevap, function (i, Bkat) {

                $("#bkat_id").append(
                    $('<option></option>').val(Bkat.ID).html(Bkat.bkat));

            })

        }
    });
}