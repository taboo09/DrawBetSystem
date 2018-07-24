
$(document).ready(function(){
    getRows();
    getNumberTd();
});

/****************** Bets *******************/

// Display the nr of rows
function getRows(){
    $('.bets span.bet-title').empty();
    var trs = $('tr').filter(function(){
        return $(this).css('display') != 'none';
    }).length-1;
    $('.bets span.bet-title').append(trs);
};

// top of the table links
$('.display-options-bets span.unsettled').click(function(){
    $('table tbody tr.unsettled').show();
    $('table tbody tr.settled').hide();
    getRows();
});
$('.display-options-bets span.all').click(function(){
    $('table tr').show();
    tableSort('first-child','asc');
    getRows();
});
$('.display-options-bets span.last10').click(function(){
    tableSort('first-child','asc');
    $('table tbody tr').hide();
    $('table tbody tr').slice(-10).show();
    getRows();
});

// Display all bets when team name is clicked
function display_bets_team(td_team){
    var a = $('table tbody tr').hide();
    a.each(function() {
        if ($(this).find('td.select_team').text() == td_team) {
            $(this).show();
        }
    });
    tableSort('first-child','asc');
    getRows();
}

// Sort table
function tableSort(position,type){
    var table = $('table');
    var rows = table.find('tr:gt(0)').toArray().sort(comparer($('td:'+position).index()));
    if(type != 'asc') { rows = rows.reverse(); }
    else var rows = table.find('tr:gt(0)').toArray().sort(comparer($('td:'+position).index()));
    for (var i = 0; i < rows.length; i++){
        table.append(rows[i]);
    }
}
function comparer(index) {
    return function(a, b) {
        var valA = getCellValue(a, index);
        var valB = getCellValue(b, index);
        return valA - valB;
    }
}
function getCellValue(row, index){ 
    if(index == 9) return parseFloat($(row).children('td').eq(index).text().slice(1)); 
    return parseFloat($(row).children('td').eq(index).text());
}

// display popout div for edit and delete actions
$('input.edit').click(function(){
    $('div.popout').css('display','block');
});
$('td.delete-bet button').click(function(){
    $('div.popout').css('display','block');
});
$('span.close-season').click(function(){
    $('div.popout').css('display','block');
});

// delete button
var checkbox = $("#delbet");
checkbox.click(function(){
    var a = $('table tbody tr').find('td.delete-bet');
    a.toggle(this.checked);
})


/*********** Index **********************************/

// change currency
$('p.show-div').click(function(){
    var a = $('div.show-toggle').toggle("slow");
})

// apply index for table rows
function getNumberTd(){
    var i = 1;
    $('div.team-status table tbody tr').each(function(){
        $(this).find('td:first').append(i++);
    });
}   

// display div info
function div_show(nr){
    $('.team-info').addClass('hidden');
    $('#team'+ nr).fadeIn(500,'linear').removeClass('hidden').css('display','inline-block');
}
function div_hide(nr){
    // $('#team'+ nr).addClass('hidden');
}

/*************** Add Team ************/

var bootstrapButtons = [ 'primary', 'secondary', 'success', 'info',
'warning', 'danger', 'link'];
// apply btn class 
var $teamBtns = $('div.teams-display button');
var index = 0;
$teamBtns.each(function(){
    if (index == bootstrapButtons.length) { index = 0; }
    $(this).addClass('btn btn-' + bootstrapButtons[index]);
    index++;
});