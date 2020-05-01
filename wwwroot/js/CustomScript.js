function ConfirmDelete(unique, isDeleteClick) {
    var confirm = "confirmDelete_" + unique;
    var Delete = "delete_" + unique;
    if (isDeleteClick) {
        $('#' + confirm).show();
        $('#' + Delete).hide();
    } else {
        $('#' + confirm).hide();
        $('#' + Delete).show();
    }
}