
function aracDegistir(a_id) {
    var marka = document.getElementById('marka_' + a_id);
    var model = document.getElementById('model_' + a_id);
    var plaka = document.getElementById('plaka_' + a_id);
    var yil = document.getElementById('yil_' + a_id);
    var ytip = document.getElementById('ytip_' + a_id);
    var vtip = document.getElementById('vtip_' + a_id);
    var maxyas = document.getElementById('maxyas_' + a_id);
    var durum = document.getElementById('durumlar_' + a_id);
    var fiyat = document.getElementById('fiyat_' + a_id);
    if ((marka.value != "") && (model.value != "")) {
        var dataBilgi = {};
        dataBilgi.d = { ID: a_id, marka: marka.value, model: model.value, plaka: plaka.value, yil: yil.value, ytip: ytip.value, vtip: vtip.value, maxyas: maxyas.value, durum: durum.value, fiyat: fiyat.value };
        $.ajax({
            url: '/araclar/degistir',
            type: 'POST',
            data: dataBilgi,
            beforeSend: function () {

            },
            success: function (cevap) {
                if (cevap == "Yetkisiz giriş") {

                    Window.location = "/Admin/login";

                }
                else {

                    bootbox.alert(cevap);
                }
            },
            error: function () {
                bootbox.alert('Hata 89');
            }
        });
    }
    else {
        bootbox.alert('Boş alanları doldurunuz')
    }

}
    


function imajYenile(id) {

    var formData = new FormData();
    var imaj = document.getElementById('imaj_' + id);
    var foto = document.getElementById('foto_' + id);



    formData.append(imaj.files[0].name, imaj.files[0]);

    formData.append("id", id);

    var xhr = new XMLHttpRequest();

    xhr.open('POST', '/araclar/imaj_degistir');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            foto.src = '/imajlar/' + imaj.files[0].name;
            bootbox.alert("İmaj Güncellendi.");

        }
    }
}
function aracSil(id) {
    bootbox.confirm('Aracı silmek istiyor musunuz?', function (cevap) {

        if (cevap) {

            $.ajax({
                url: '/araclar/sil/' + id,
                type:'POST',
                beforeSend: function () {

                },
                success: function (sonuc) {
                    if (sonuc == "Yetkisiz giriş") {

                        Window.location = "/Admin/login";

                    }
                    else {
                        bootbox.alert(sonuc);
                        $('#a_' + id).remove();
                    }
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