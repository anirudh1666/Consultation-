

function populate_tables(loc){
    populate_table_with_parameters(loc, "query");
    populate_table_with_parameters(loc, "url")
}


function populate_table_with_parameters(loc=0, table) {
    
    $.ajax({
        url: 'get/ajax/get/table/data',
        type: 'GET',
        data: {'location': loc, 'table': table},
        success: function(json) {
            for (let time = 1; time < Object.keys(json).length+1; time++) {
                for (let i = 0; i < 10; i++) {
                    let identifier = table + "-" + time + "-" + i;
                    let diseaseTerm = (Object.keys(json[String(time)])[i] ? make_key_readable(Object.keys(json[String(time)])[i]) : "No data")
                    document.getElementById(identifier).innerHTML = (i+1) + ". " + diseaseTerm;
                }
            }
        }
     });
}

function make_key_readable(key){
    return_array = [];
    key.split("_").forEach(word => {
        return_array.push(word.charAt(0).toUpperCase() + word.slice(1));
    });
    return return_array.join(" ");
}