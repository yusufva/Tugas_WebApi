const Base = "https://localhost:7157/"

const login = {
    "email": "nature.p@mail.com",
    "password": "1234567890"
}

let token
$.ajax({
    url: Base + "api/Accounts/login",
    method: "POST",
    contentType: 'application/json',
    dataType: "json",
    async: false,
    data: JSON.stringify(login),
    //success: (data) => {
    //    return data.data
    //}
}).done((resp) => {
    token = resp.data.token
}).fail((error) => {
    console.log(error);
})

const bearer = "bearer " + token

// UNUSED
//let employees
//$.ajax({
//    url: Base + "api/Employee",
//    contentType: 'application/json',
//    method: "GET",
//    beforeSend: (req) => {
//        req.setRequestHeader("Authorization", bearer)
//    },
//    async: false,
//    //success: (data) => {
//    //    console.log(data)
//    //}
//}).done((res) => {
//    employees = res.data
//})

$('#employeeTable').DataTable({
    ajax: {
        "url": Base + "api/Employee",
        "type": "GET",
        'beforeSend': function (request) {
            request.setRequestHeader("Authorization", bearer);
        },
        "dataSrc": "data"
    },
    columns: [
        {
            data: null,
            render: function (data, type, row, meta) {
                return meta.row + 1;
            },
            visible: true
        },
        { data: "nik", visible: true },
        { data: "firstName", visible: true },
        { data: "lastName", visible: true },
        { data: "email", visible: true },
        { data: "phoneNumber", visible: true },
        {
            data: null,
            render: (data, type, row, meta) => {
                return `<button type="button" onclick="detail('${data.guid}')" class="btn btn-info" data-toggle="modal" data-target="#modalEmployee">Edit</button>
                        <button type="button" onclick="confirmDelete('${data.guid}')" class="btn btn-danger" data-toggle="modal" data-target="#modalDeleteEmployee">Delete</button>
                `;
            },
            visible: true
        }
    ],
    dom: 'Bfrtip',
    buttons: {

        buttons: [
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 4, 5]
                },
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 4, 5]
                },
            },
            'colvis'
        ],
        dom: {
            button: {
                className: 'btn btn-outline-success'
            }
        }
    },
})

$('.dt-buttons').addClass('btn-group row mt-3')
$('.dt-buttons').removeClass('dt-buttons')
$('.dt-button-collection').addClass('row-cols-4')



function openCreate() {
    $('#employeeLabel').html("Create New Employee")
    $('#nik').val("")
    $('#firstName').val("")
    $('#lastName').val("")
    $('#birthDate').val("")
    $('#gender').val("")
    $('#hireDate').val("")
    $('#email').val("")
    $('#phoneNumber').val("")
    $('buttonUpdate').html(
        `
            <button type="button" onclick="Insert()" class="btn btn-primary mr-2">Insert</button>
            <button class="btn btn-light">Cancel</button>
        `
    )
}

function detail(guid) {
    let employee
    $.ajax({
        url: Base + "api/Employee/" + guid,
        method: "GET",
        beforeSend: (req) => {
            req.setRequestHeader("Authorization", bearer)
        },
        async: false,
        //success: (data) => {
        //    console.log(data)
        //}
    }).done((res) => {
        employee = res.data
    })

    let birthFormatted = employee.birthDate.split('T')[0];
    let hireFormatted = employee.hiringDate.split('T')[0];
    let employeeFullName = employee.firstName + " " + employee.lastName

    $('#employeeLabel').html("Edit " + employeeFullName)
    $('#nik').val(employee.nik)
    $('#firstName').val(employee.firstName)
    $('#lastName').val(employee.lastName)
    $('#birthDate').val(birthFormatted)
    $('#gender').val(employee.gender)
    $('#hireDate').val(hireFormatted)
    $('#email').val(employee.email)
    $('#phoneNumber').val(employee.phoneNumber)
    $('#buttonUpdate').html(
        `
            <button type="button" onclick="Update('${guid}')" class="btn btn-primary mr-2" id="submit">Update</button>
            <button class="btn btn-light" data-dismiss="modal">Cancel</button>
        `
    )
}

function Update(guid) {
    let data = {
        guid: guid,
        nik: $('#nik').val(),
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        birthDate: $("#birthDate").val(),
        gender: parseInt($("#gender").val()),
        hiringDate: $("#hireDate").val(),
        email: $("#email").val(),
        phoneNumber: $("#phoneNumber").val(),
    };

    $.ajax({
        url: Base + "api/Employee",
        type: "PUT",
        beforeSend: (req) => {
            req.setRequestHeader("Authorization", bearer)
        },
        contentType: 'application/json',
        async: false,
        dataType: "json",
        data: JSON.stringify(data)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            title: 'Success!',
            text: result.message,
            icon: 'success',
            confirmButtonText: 'OK!'
        })
        $("#submit").attr("data-dismiss", "modal");
        $('#employeeTable').DataTable().ajax.reload();
    }).fail((result) => {
        //alert pemberitahuan jika gagal
        //console.log(result.responseJSON)
        $("#failMessage").removeClass("alert-danger, alert-warning, alert-success").addClass("alert-danger").text(result.responseJSON.message[1] /*+ ", " + "All Field must be set"*/).show();
    })
}

//ini kodingan dari tombol 'insert' yang ada atribut onclick="Insert()"
function Insert() {
    let data = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    data.firstName = $("#firstName").val();
    data.lastName = $("#lastName").val();
    data.birthDate = $("#birthDate").val();
    data.gender = parseInt($("#gender").val());
    data.hiringDate = $("#hireDate").val();
    data.email = $("#email").val();
    data.phoneNumber = $("#phoneNumber").val();

    $.ajax({
        url: Base + "api/Employee",
        type: "POST",
        beforeSend: (req) => {
            req.setRequestHeader("Authorization", bearer)
        },
        contentType: 'application/json',
        async: false,
        dataType: "json",
        data: JSON.stringify(data) //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            title: 'Success!',
            text: result.message,
            icon: 'success',
            confirmButtonText: 'OK!'
        })
        $("#submit").attr("data-dismiss", "modal");
        $('#employeeTable').DataTable().ajax.reload();
    }).fail((result) => {
        //alert pemberitahuan jika gagal
        //console.log(result.responseJSON)
        $("#failMessage").removeClass("alert-danger, alert-warning, alert-success").addClass("alert-danger").text(result.responseJSON.message[1] /*+ ", " + "All Field must be set"*/).show();
    })
}

function confirmDelete(guid) {
    Swal.fire({
        title: 'Confirm to Delete this Data?',
        text: "Deleted Data will be lost",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it',
        cancelButtonText: 'No, cancel',
        reverseButtons: true
    }).then((result => {
        if (result.dismiss === Swal.DismissReason.cancel) {
            return
        }
        $.ajax({
            url: Base + "api/Employee/" + guid,
            type: "DELETE",
            beforeSend: (req) => {
                req.setRequestHeader("Authorization", bearer)
            },
        }).done((res) => {
            Swal.fire({
                title: 'Success!',
                text: res.message,
                icon: 'success',
                confirmButtonText: 'OK!'
            })
            $('#employeeTable').DataTable().ajax.reload();
        }).fail((result) => {
            Swal.fire({
                title: 'Failed!',
                text: result.responseJSON.message,
                icon: 'error',
                confirmButtonText: 'OK!'
            })
            $('#employeeTable').DataTable().ajax.reload();
        })
    }))
}