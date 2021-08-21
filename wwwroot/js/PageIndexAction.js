
$(function () {
    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("Do you want to delete this")) {
                // when the user clicked cancel in popup
                return false;
            }
        });
    }
    if ($("div.alert.notification").length) {
        $("div.alert.notification").fadeOut(2000);
    }

});
