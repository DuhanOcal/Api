var table;

$("#btnGetCustomer").click(function ()  {
    table = $('#example').DataTable({
        paging: false,
        ajax: {
            url: "https://localhost:44392/api/Customer/GetAllCustomer",
            type: "GET",
            dataSrc: "", // API'den dönen verilerin veri kaynağını belirtir
            error: function (xhr, status, error) {
                alert("Hata : API " + error);
            }
        },
        columns: [
            { data: 'name' },
            { data: 'surname' },
            { data: 'mail' },
            { data: 'phoneNumber' }
        ]
    });

    // Form gönderimini ele al
    $('#userForm').on('submit', function (e) {
        e.preventDefault(); // Formun normal gönderim işlemini engeller

        var formData = {
            name: $('#name').val(),
            surname: $('#surname').val(),
            mail: $('#mail').val(),
            password: $('#password').val(),
            phoneNumber: $('#phoneNumber').val()
        };

        $.ajax({
            url: "https://localhost:44392/api/Customer/AddCustomer",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                Swal.fire({
                    icon: "success",
                    title: "Bilgileriniz kaydedildi",
                });
                table.ajax.reload(); // DataTable'ı yeniden yükle
            },
            error: function (xhr, status, error) {
                Swal.fire("Hata!", "Müşteri eklenirken bir hata oluştu.", "error");
            }
        });
    });
})


$("#btnGetSeller").click(function ()  {
    table2 = $('#example2').DataTable({
        paging: false,
        ajax: {
            url: "https://localhost:44392/api/Seller/GetAllSeller",
            type: "GET",
            dataSrc: "", // API'den dönen verilerin veri kaynağını belirtir
            error: function (xhr, status, error) {
                alert("Hata : API " + error);
            }
        },
        columns: [
            { data: 'name' },
            { data: 'taxNumber' },
            { data: 'mail' },
            { data: 'phoneNumber' },
            { data: 'adress' }
        ]
    });
})



    $('#signIn').click(function() {
        const inputRadios = document.getElementsByName("userType");
        let isChecked = null
        for (const item of inputRadios) {
            if (item.checked) isChecked = item.value
        }
        var formData = {
            mail: $('#loginMail').val(),
            password: $('#loginPassword').val(),
            UserType: isChecked
        };
      //  window.location.href = 'home.html', succes olursa bunu aktive edicem
        console.log(formData) 

        $.ajax({
            url: "https://localhost:44392/api/Account/LoginAccount",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (result) {
                console.log(result);
                
                // Başarı mesajını göster
                Swal.fire({
                    title: "BAŞARILI!",
                    text: result.message || "Giriş başarılı",
                    icon: "success",
                    showConfirmButton: false,
                    timer: 1000, // Mesajın gösterim süresi (2 saniye)
                    timerProgressBar: true,
                }).then(() => {
                    if (formData.UserType == 1 || formData.UserType == 2) {
                        window.location.href = "home.html"; // Yönlendirmek istediğiniz sayfa
                    }
                });
            },
            error: function (result) {
                console.log("Hata:", result);
                Swal.fire({
                    title: "UYARI!",
                    text: result.responseText || "Bir hata oluştu",
                    icon: "warning",
                 //   confirmButtonColor: "#556ee6",
                  //  confirmButtonText: "OK",
                });
            }
        });
    });

    // customer olarak kayıt olma

     $('#signUp').click(function(){
        
        window.location.href= "signup.html"
     })

        $('#signUpCustomer').click(function () {
            // Form verilerini al
            var customerUserForm = {
                name: $('#name').val(),
                surname: $('#surname').val(),
                mail: $('#mail').val(),
                password: $('#password').val(),
                phoneNumber: $('#phoneNumber').val()
            };
        
            $.ajax({
                url: "https://localhost:44392/api/Customer/AddCustomer",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(customerUserForm), // Verileri JSON formatında gönder
                success: function (result) {
                    console.log("Başarıyla Yanıt Alındı:", result);
                    Swal.fire({
                        title: "BAŞARILI!",
                        text: result.message || "Kayıt Başarıyla Tamamlandı",
                        icon: "success",
                        confirmButtonColor: "#556ee6",
                        confirmButtonText: "OK"
                    }).then(() => {
                        window.location.href = "login.html"; // Yönlendirmek istediğiniz sayfa
                    });
                },
                error: function (xhr, status, error)  {
                    console.log("error Yanıt Alındı:", error);
                    Swal.fire({
                        title: "HATA!",
                        text: xhr.responseText || "Bir hata oluştu",
                        icon: "error",
                        confirmButtonColor: "#556ee6",
                        confirmButtonText: "OK"
                    });
                }
            });
        });

        $('#signUpSeller').click(function () {
            // Form verilerini al
            var customerUserForm = {
                name: $('#name').val(),
                taxnumber: $('#taxnumber').val(),
                mail: $('#mail').val(),
                password: $('#password').val(),
                phoneNumber: $('#phoneNumber').val(),
                adress: $('#adress').val()
            };
        
            $.ajax({
                url: "https://localhost:44392/api/Seller/AddSeller",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(customerUserForm), // Verileri JSON formatında gönder
                success: function (result) {
                    console.log("Başarıyla Yanıt Alındı:", result);
                    Swal.fire({
                        title: "BAŞARILI!",
                        text: result.message || "Kayıt Başarıyla Tamamlandı",
                        icon: "success",
                        confirmButtonColor: "#556ee6",
                        confirmButtonText: "OK"
                    }).then(() => {
                        window.location.href = "login.html"; // Yönlendirmek istediğiniz sayfa
                    });
                },
                error: function (xhr, status, error)  {
                    console.log("error Yanıt Alındı:", error);
                    Swal.fire({
                        title: "HATA!",
                        text: xhr.responseText || "Bir hata oluştu",
                        icon: "error",
                        confirmButtonColor: "#556ee6",
                        confirmButtonText: "OK"
                    });
                }
            });
        });        