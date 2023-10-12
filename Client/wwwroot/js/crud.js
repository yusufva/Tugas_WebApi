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
let employees

$.ajax({
    url: Base + "api/Employee",
    method: "GET",
    beforeSend: (req) => {
        req.setRequestHeader("Authorization", bearer)
    },
    async: false,
    //success: (data) => {
    //    console.log(data)
    //}
}).done((res) => {
    employees = res.data
})

$('#employeeTable').DataTable({
    data: employees,
    columns: [
        { data: "nik" },
        { data: "firstName" },
        { data: "lastName" },
        { data: "email" },
        { data: "phoneNumber" },
        {
            data: null,
            render: (data, type, row, meta) => {
                return `<button type="button" onclick="detail('${data.guid}')" class="btn btn-primary" data-toggle="modal" data-target="#modalEmployee">Edit</button>`;
            }
        }
    ]
})

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
    console.log(employee)

    let birthFormatted = employee.birthDate.split('T')[0];
    let hireFormatted = employee.hiringDate.split('T')[0];

    $('#nik').val(employee.nik)
    $('#firstName').val(employee.firstName)
    $('#lastName').val(employee.lastName)
    $('#birthDate').val(birthFormatted)
    $('#gender').val(employee.gender)
    $('#hireDate').val(hireFormatted)
    $('#email').val(employee.email)
    $('#phoneNumber').val(employee.phoneNumber)
    $('buttonUpdate').html(
        `
            <button type="button" onclick="" class="btn btn-primary mr-2">Update</button>
            <button class="btn btn-light">Cancel</button>
        `
    )
}