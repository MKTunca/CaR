
function ckatDegistir(ck_id) {
    var ckat = document.getElementById('ckat_' + ck_id);
    var dataBilgi = {};
    dataBilgi.c = { ID: ck_id, ckat: ckat.value };
    if (ckat.value != '') {
        $.ajax({
            url: '/kategoriler/cDegistir',
            data: dataBilgi,
            type: 'POST',
            success: function (cevap) {
                bootbox.alert('C kategori güncellendi')
            },
            error: function () {

            }
        });
    }
}
function ckatSil(ck_id) {
    bootbox.confirm('C kategoriyi silmek istiyor musunuz?', function (cevap) {

        if (cevap) {

            $.ajax({
                url: '/kategoriler/cSil',
                data: { id: ck_id },
                success: function (sonuc) {

                    bootbox.alert(sonuc);
                    $('#ck_' + ck_id).remove();
                },
                error: function () {

                }
            });

        }
        else {
            bootbox.alert('Silme işlemi iptal edildi.');
        }

    })
}