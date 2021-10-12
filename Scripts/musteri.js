
function musteriDegistir(mus_id) {
    var adsoyad = document.getElementById('adsoyad_' + mus_id);
    var email = document.getElementById('email_' + mus_id);
    var sifre = document.getElementById('sifre_' + mus_id);
    var telno = document.getElementById('telno_' + mus_id);
    var etipi = document.getElementById('etipi_' + mus_id);
    var dyil = document.getElementById('dyil_' + mus_id);
    var il = document.getElementById('il_' + mus_id);
    var ilce = document.getElementById('ilce_' + mus_id);
    var adres = document.getElementById('adres_' + mus_id);
    var durum = document.getElementById('durumlar_' + mus_id);
    if ((email.value != '') && (sifre.value != '')) {

        var dataBilgi = {};
        dataBilgi.deg = { ID: mus_id, adsoyad: adsoyad.value, email: email.value, sifre: sifre.value, telno: telno.value, etipi: etipi.value, dyil: dyil.value, il: il.value, ilce: ilce.value, adres: adres.value, durum: durum.value };

        $.ajax({

            url: '/musteriler/degistir',
            data: dataBilgi,
            type: 'POST',
            success: function (cevap) {

                bootbox.alert('Bilgileriniz güncellendi');
            },
            error: function () {

            }
        });

    }
    else {
        bootbox.alert('Alanları boş bırakmayınız!');
    }

}
function musteriSil(id) {
    bootbox.confirm('Müşteriyi silmek istiyor musunuz?', function (cevap) {

        if (cevap) {

            $.ajax({
                url: '/musteriler/sil/' + id,
                type:'POST',
                success: function (sonuc) {

                    bootbox.alert(sonuc);
                    $('#mus_' + id).remove();
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