const token = '@Context.Session.GetString("JWToken")';

$.ajax({
    url: "university/getall",
    'beforeSend': function (request) {
        request.setRequestHeader("Authorization", "Bearer " + token);
    },
}).done((res) => {
    console.log(res)
})

$('#universityTable').DataTable({
    ajax: {
        "url": "university/getall",
        "type": "GET",
        //'beforeSend': function (request) {
        //    request.setRequestHeader("Authorization", "Bearer " + token);
        //},
        "dataSrc": ""
    },
    columns: [
        {
            data: null,
            render: function (data, type, row, meta) {
                return meta.row + 1;
            },
            visible: true
        },
        { data: "name", visible: true },
        { data: "code", visible: true },
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