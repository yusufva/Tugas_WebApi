//$("h1").html("ini saya ubah dengan jquery")

$(document).ready(() => {
    $('#pokeTable').DataTable({
        ajax: {
            url: 'https://pokeapi.co/api/v2/pokemon?offset=0&limit=2000',
            dataSrc: 'results'

        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: 'name',
                render: function (data, type, row) {
                    return data.charAt(0).toUpperCase() + data.slice(1);
                }
            },
            {
                render: (data, type, row) => {
                    return `<button type="button" onclick="detail('${row.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button>`
                }
            }
        ],
        //order: [[1, 'asc']],
        DOM: 'Bfrtip'
    })
})


//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon?offset=0&limit=2000",
//    //success: (result) => {
//    //    console.log(result);
//    //}
//}).done((result) => {
//    let temp = "";
//    $.each(result.results, (key, val) => {
//        temp += `<tr>
//                    <td>${key + 1}</td>
//                    <td>${val.name[0].toUpperCase() + val.name.slice(1)}</td>
//                    <td><button type="button" onclick="detail('${val.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
//                </tr >`;
//    });

//    $('#tbodyPoke').html(temp);

//}).fail((error) => {
//    console.log(error);
//})

function detail(url) {
    $.ajax({
        url: `${url}`,
    }).done((ress) => {
        let name = ress.species.name[0].toUpperCase() + ress.species.name.slice(1)
        let sprite = `<img src="${ress.sprites.other['official-artwork'].front_default}" class="img-thumbnail" alt="${name}">`
        let types = ""
        let moves = ""
        let stats = ""

        $.each(ress.moves, (key, val) => {
            moves += `<ul class="list-group col-4"><li class="list-group-item fs-4">${val.move.name}</li></ul>`
        })

        $.each(ress.types, (key, val) => {
            types += `<span class="badge rounded-pill text-bg-${val.type.name} fs-4">${val.type.name[0].toUpperCase() + val.type.name.slice(1)}</span>`
        })

        $.each(ress.stats, (key, val) => {
            stats += `
                <div class="progress" role="progressbar" aria-label="Example with label" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100" style="height: 40px">
                  <div class="progress-bar fs-5 fw-bold overflow-visible bg-${val.stat.name}" style="width: ${val.base_stat / 255 * 100}%">&nbsp${val.stat.name[0].toUpperCase() + val.stat.name.slice(1)}</div>
                </div><br>
            `
        })

        $('#pokeLabel').html(name)
        $('#pokeSprites').html(sprite)
        $('#types').html(types)
        $('#pokeMoves').html(moves)
        $('#pokeStats').html(stats)
    }).fail((err) => {
        console.log(err)
    })
}