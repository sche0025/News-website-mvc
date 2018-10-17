


$(document).ready(function () {

    $('#data tfoot th').each(function (i) {
        var title = $('#data thead th').eq($(this).index()).text().trim();
        $(this).html('<input type="text" placeholder="Search '+title+'" data-index="'+i+'"/>')
    });

    var table = $('#data').DataTable({
  
      "sPaginationType": "full_numbers",
      "order":[[1,"desc"]],
        "sDom": '<"nav"lf>t<"nav"ip>',
    });

    $(table.table().container()).on('keyup', 'tfoot input', function () {
       table.column($(this).data('index')).search(this.value).draw();
    })


   // var table = $('#data').dataTable();
   // var tableTools = new $.fn.dataTable.tableTools(table);
    //$(tableTools.fnContainer()).insertBefore('#data_wrapper');

    $('#data tbody tr:even').mouseover(function () {
        
        $(this).addClass('cyan');
    });

    $('#data tbody tr:even').mouseout(function () {
        $(this).removeClass('cyan');
    });

    $('#data tbody tr:odd').mouseover(function () {
   
        $(this).addClass('cyan');
    });
    $('#data tbody tr:odd').mouseout(function () {
        $(this).removeClass('cyan');

    });

});