//$("h1").html("ini saya ubah dengan jquery")


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon?offset=0&limit=2000",
    //success: (result) => {
    //    console.log(result);
    //}
}).done((result) => {
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name[0].toUpperCase() + val.name.slice(1)}</td>
                    <td><button type="button" onclick="detail('${val.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
                </tr >`;
    });

    $('#tbodyPoke').html(temp);

}).fail((error) => {
    console.log(error);
})

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
            moves += `<li class="list-group-item fs-3">${val.move.name}</li>`
        })

        $.each(ress.types, (key, val) => {
            types += `<span class="badge rounded-pill text-bg-${val.type.name} fs-3">${val.type.name[0].toUpperCase() + val.type.name.slice(1)}</span>`
        })

        $.each(ress.stats, (key, val) => {
            stats += `
                <div class="progress" role="progressbar" aria-label="Example with label" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100" style="height: 40px">
                  <div class="progress-bar fs-3 fw-semibold overflow-visible bg-${val.stat.name}" style="width: ${val.base_stat / 255 * 100}%">&nbsp${val.stat.name[0].toUpperCase() + val.stat.name.slice(1)}</div>
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