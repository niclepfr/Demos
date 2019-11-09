function PageResize() {
    var screenh = parseInt($(window).height());
    var _screenh = parseInt($(window).height() + window.screenTop);
    var __screenh = parseInt($(window).height() + ($(window).scrollTop()));
    var h = 0;
    //$('section.content-holder').css('min-height', '0px');
    $('section.content-holder').attr('style', '');
    $('section').each(function (_index, _obj) {
        h = parseInt(h + $(_obj).outerHeight(true));
    });
    if (h < screenh) {
        $('section.content-holder').css('min-height', parseInt((screenh - h) + $('section.content-holder').outerHeight(true)) + 'px');
        //$('section.content-holder').attr('style','min-height:' + parseInt((screenh - h) + $('section.content-holder').outerHeight(true)) + 'px');
    }
}
function SlideLoad() {
    $('[data-orbit]').each(function (_index, _obj) {
        var _imgH = 0;
        $(_obj).find('figure img').each(function (__index, __obj) {
            var __imgH = parseInt($(__obj).outerHeight());
            if (__imgH > 0) {
                if (__index == 0) {
                    _imgH = __imgH;
                } else {
                    if (_imgH > __imgH) {
                        _imgH = __imgH;
                    }
                }
            }
        });
        if (_imgH > 0) {
            $(_obj).find('figure img').each(function (__index, __obj) {
                $(__obj).css('max-height', _imgH + 'px');
            });
            $(_obj).find('.orbit-container').css('height', _imgH + 'px')
        }


        $($('[data-orbit]')[_index]).on('slidechange.zf.orbit', function (__e, __obj) {
            __e.preventDefault();
        });
    });
}
function SlideResize() {
    $('[data-orbit]').each(function (_index, _obj) {
        var _imgH = parseFloat($(_obj).find('.orbit-container').css('height'));
        if (_imgH > 0) {
            $(_obj).find('figure img').each(function (__index, __obj) {
                $(__obj).css('max-height', _imgH + 'px');
            });
        }
    });
}
function SkinPageReady() {
    PageResize();
    $(window).resize(function () {
        PageResize();
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() > $(window).outerHeight()) {
            $('a.ws-page-scrollup').toggleClass('ws-hide', false).toggleClass('ws-show', true).css('bottom', '20px');
            if (($(window).scrollTop() + ($(window).outerHeight() - 75)) > (parseInt($('section.header-holder').outerHeight(true) + $('section.content-holder').outerHeight(true)))) {
                $('a.ws-page-scrollup').css('bottom', '120px');
            }
        } else {
            if ($('a.ws-page-scrollup.ws-show').length > 0) {
                $('a.ws-page-scrollup').toggleClass('ws-show', false).toggleClass('ws-hide', true);
            }
        }
    });
    $('.moncv .head .tof img').attr('title', 'Nicolas Leprêtre, ' + parseInt((new Date().getFullYear()) - 1972) + ' ans' + ', Célibataire');
    //$('.moncv .head .tof img').on('mouseenter', function (e) { 
    //    $(this).attr('title', 'Nicolas Leprêtre, ' + parseInt((new Date().getFullYear())-1972) + ' ans' + ', Célibataire');
    //});
    //$('.moncv .head .tof img').on('mouseleave', function (e) {
    //    $(this).attr('title', '');
    //});
}