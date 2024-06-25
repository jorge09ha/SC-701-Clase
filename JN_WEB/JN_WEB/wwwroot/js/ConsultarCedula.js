function ConsultarCedula() {
    let cedula = $("#Cedula").val();

    $.ajax({
        url: "https://apis.gometa.org/cedulas/" + cedula,
        method: "GET",
        datatype: "json",
        success: function (response) {
            // Asumimos que solo hay un resultado en la respuesta
            if (response.results && response.results.length > 0) {
                let result = response.results[0];
                let fullName = "";

                if (response.tipoIdentificacion === "01") { // Persona física
                    fullName = formatName(result.firstname1);
                    if (result.firstname2) {
                        fullName += " " + formatName(result.firstname2);
                    }
                    if (result.lastname1) {
                        fullName += " " + formatName(result.lastname1);
                    }
                    if (result.lastname2) {
                        fullName += " " + formatName(result.lastname2);
                    }
                } else if (response.tipoIdentificacion === "02") { // Persona jurídica
                    // Asegurarse de aplicar formateo también a nombres de personas jurídicas
                    fullName = formatName(result.fullname || response.nombre);
                }

                $("#Nombre").val(fullName.trim());
            }
        },
        error: function (xhr, status, error) {
            console.error("Error al consultar la cédula: ", error);
        }
    });
}

function formatName(name) {
    if (!name) return "";
    // Dividir el nombre en partes para capitalizar cada parte y luego unirlas
    return name.toLowerCase().split(' ').map(part => part.charAt(0).toUpperCase() + part.slice(1)).join(' ');
}
