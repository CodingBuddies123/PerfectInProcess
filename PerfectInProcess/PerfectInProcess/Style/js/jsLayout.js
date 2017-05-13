function toggleSecondaryMenu() {
    $('.menu-togglesidebar-btn').toggleClass("hidden");
    $('.w3-overlay').toggleClass("overlay-toggle-show");
    $('.menu-secondary-container').toggleClass("hidden");

    $('.PermissionGroup').addClass("hidden");
    $('.inactive-carrot').removeClass('hidden');
    $('.active-carrot').addClass('hidden');

}
function toggleSecondaryMenuGroup(x) {
    var id = $(x).attr('id');
    if ($('.' + id).hasClass('hidden')) {
        $('.PermissionGroup').addClass('hidden');
        $('.' + id).removeClass('hidden');
        $('.inactive-carrot').removeClass('hidden');
        $('.active-carrot').addClass('hidden');
        $(x).children('span.active-carrot').removeClass('hidden');
        $(x).children('span.inactive-carrot').addClass('hidden');
    }
    else {
        $('.PermissionGroup').addClass("hidden");
        $('.inactive-carrot').removeClass('hidden');
        $('.active-carrot').addClass('hidden');
    }
}