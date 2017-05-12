$(document).ready(function () {
    SizeLayoutElements();
    ResizeLogo();
})

$(window).resize(function () {
    SizeLayoutElements();
    ResizeLogo();
})

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

function SizeLayoutElements() {
    var menuMainHeight = '60';
    var menuSeondaryMinWidth = '300';
    var logoFontSize = '40';

    var pageHeight = $(window).height();
    var bodyUsableHeight = pageHeight - menuMainHeight;

    $('.menu-main-height').height(menuMainHeight + 'px');
    $('.menu-secondary-container').width(menuSeondaryMinWidth + 'px');
    $('.menu-secondary-container').height(bodyUsableHeight + 'px');
    $('.logo').css("font-size", logoFontSize + 'px')
}

function ResizeLogo() {
    while ($('.logo').height() > $('.menu-main-height').height()) {
        var fontSize = parseInt($('.logo').css("font-size"));
        $('.logo').css("font-size", fontSize - 1 + 'px')
    }
}