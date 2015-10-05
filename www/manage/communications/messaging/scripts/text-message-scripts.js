function keyUP(txt) {
    var str = txt.value;
    str.replace("’", "'");
    var curr_count = 160 - str.length;
    document.getElementById('charCount').innerHTML = curr_count;
}

function countChar(str, f) {
    f.Count.value = 160 - str.length
}