/*!
 * SD.Scroll 1.0
 * Animation pour faire défiler horizontalement ou verticalement un contenu
 * 2015-2015 niclep
 * Version: 1.0.0.4 (23-AVRIL-2015)
 * Requires:
 * jQuery v1.2.x ou superieur
 * CSS Foundation v5
 * Exemple utilisation :
 * $('ul#id_tag').sdScroll({ duration: 20000, scroll: "horizontal", direction: "left", double_scroll: true, fade_border: true, pause_onhover: true, div_bgcolor: 'rgb(255,255,255)' });
 * Template html :
 * <div class="columns">
 *   <div class="sdScrollH-BordGauche"></div>
 *   <div class="sdScrollH-BordDroite"></div>
 *   <div class="sdScrollH-Layer"></div>
 *   <div class="sdScrollH-PlayPause"><span></span></div>
 *   <ul>
 *   <li><p>Texte du message défilant</p></li>
 *   </ul>
 * </div>
 *!
 */

jQuery.fn.sdScroll = function (params) {
    params = jQuery.extend({
        duration: 20000, // The default duration is 400 milliseconds. The strings 'fast' and 'slow' can be supplied to indicate durations of 200 and 600 milliseconds, respectively.
        scroll: "horizontal", // vertical
        direction: "right", // right: de gauche à droite - left: droite à gauche - top: de bas en haut - bottom: de haut en bas
        double_scroll: false, // effet continu
        fade_border: false, // fondu sur les bordures
        pause_onhover: true,
        div_bgcolor: 'rgb(255, 255, 255)'
    }, params);
    return this.each(function () {

        var did = "";
        var uid = "";
        var direction = "";
        var screen = 0;
        var bEffetContinu = params.double_scroll;
        var divbg = "";
        
        divbg += "linear-gradient(to " + params.direction + ", " + params.div_bgcolor.replace('rgb', 'rgba').replace(')', ', 1)') + ", transparent)";

        if (params.scroll == "horizontal") {

            var bScrollH = false;
            var bScrollHClone = false;

            switch (params.direction) {
                case "left":
                    direction = "-"
                    break;
                default:
                    direction = "+"
                    break;
            }

            // Applique la class pour la mise en forme et forcer l'overflow sur le conteneur parent (normalement <div class="columns"></div>)
            if ($(this).parents(".columns").length > 0) { $(this).closest("div.columns").toggleClass("sdScrollH"); }
            else { if ($(this).parents(".column").length > 0) { $(this).parents(".column").closest().toggleClass("sdScrollH"); } }
            var div = $(this).parents(".sdScrollH")
           
            //Applique la class css sur l'objet <ul>
            $(this).toggleClass("inline-list").toggleClass("sdScrollH-item");

            if (($(this).attr("id") == "") || ($(this).attr("id") == null)) { $(this).attr("id", "ul_sdScrollH" + "_" + $('.sdScrollH-item').length); }
            if (($(div).attr("id") == "") || ($(div).attr("id") == null)) { $(div).attr("id", "div_sdScrollH" + "_" + $('.sdScrollH').length); }

            did = $(div).attr("id");
            uid = $(this).attr("id");

            /*
             * Calcul la taille de la balise <ul>
            */ 

            var wul = 0; // largeur de la balise <ul>
            var h = parseFloat($(this).css("height"));
            if (h == 0) { h = 25; }
            var w = parseFloat($(this).css("width"));
            screen = parseFloat($("#" + uid).closest("div.sdScrollH").css("width")); //taille de la div où est affichée le texte défilant
           
            if ($("#" + uid + ' > li') != null) {
                var obj = $("#" + uid + ' > li');
                for (var i = 0; i < obj.length; i++) {
                    var _h = parseFloat($(obj[i]).outerHeight(true));
                    wul = parseFloat(wul + (w * (_h / h)));
                    $(obj[i]).css("width", "auto").css("float", ((direction == "+") ? "right" : "left"));
                }
            }

            $("#" + uid).css("width", (wul) + "px");
            //Au cas où il manque quelques px because les mots tronqués
            while ($("#" + uid + ' > li:first').offset().top != $("#" + uid + ' > li:last').offset().top) {
                wul = parseFloat(wul + 10);
                $("#" + uid).css("width", (wul) + "px");
            }
            //fin au cas où
            $("#" + uid).css("visibility", "visible");
            $("#" + uid).css("left", ((direction == "+") ? "-" + wul : "+" + screen) + "px");

            $(div).css('height', parseInt($(this).outerHeight(true) + 20) + 'px')

            //options
            //continu
            if (bEffetContinu) {
                //ici le clone
                $("#" + uid).clone().appendTo($("#" + uid).parent());
                $("#" + uid + '.sdScrollH-item:gt(0)').attr("id", uid + "_Clone");
            }
            //bordure
            if (params.fade_border) {
                $("#" + did + ' div[class*="sdScrollH-Bord"]').css("width", parseFloat(screen * 0.2) + "px").css("background", divbg);
                $("#" + did + ' div[class*="sdScrollH-Bord"]').css("height", $(div).css('height')).css("background", divbg);

                switch (params.direction) {
                    case 'left':
                        $("#" + did + ' div[class="sdScrollH-BordDroite"]').css("background", divbg);
                        $("#" + did + ' div[class="sdScrollH-BordGauche"]').css("background", divbg.replace('left', 'right'));
                        break;
                    case 'right':
                        $("#" + did + ' div[class="sdScrollH-BordGauche"]').css("background", divbg);
                        $("#" + did + ' div[class="sdScrollH-BordDroite"]').css("background", divbg.replace('right', 'left'));
                        break;
                }

            } else { $("#" + did + ' div[class*="sdScrollH-Bord"]').css("width", "0px"); }
            //fin options

            function sdScrollH(distance, temps) {
                $("#" + uid).animate(
                { "left": direction + "=" + distance + "px" },
                {
                    duration: temps,
                    easing: "linear",
                    step: function (now, fx) {
                        if ((bEffetContinu) && (!bScrollHClone)) {
                            if (direction == "+") {
                                if (now >= 10) { // 20 largeur de marge
                                    sdScrollHClone((wul + screen), params.duration);
                                    bScrollHClone = true;
                                }
                            }
                            else {
                                if (now <= ((screen - 10) - wul)) {
                                    sdScrollHClone((wul + screen), params.duration);
                                    bScrollHClone = true;
                                }
                            }
                        }
                    },
                    complete: function () {
                        $("#" + uid).css("left", ((direction == "+") ? "-" + wul : "+" + screen) + "px");
                        if (bEffetContinu) { bScrollH = false; }
                        else { sdScrollH((wul + screen), params.duration); } //pour la boucle
                    }
                });
            }

            function sdScrollHClone(distance, temps) {
                $("#" + uid + "_Clone").animate(
                { "left": direction + "=" + distance + "px" },
                {
                    duration: temps,
                    easing: "linear",
                    step: function (now, fx) {
                        if ((bEffetContinu) && (!bScrollH)) {
                            if (direction == "+") {
                                if (now >= 10) {
                                    sdScrollH((wul + screen), params.duration);
                                    bScrollH = true;
                                }
                            }
                            else {
                                if (now <= ((screen - 10) - wul)) {
                                    sdScrollH((wul + screen), params.duration);
                                    bScrollH = true;
                                }
                            }
                        }
                    },
                    complete: function () {
                        $("#" + uid + "_Clone").css("left", ((direction == "+") ? "-" + wul : "+" + screen) + "px");
                        bScrollHClone = false;
                    }
                });
            }

            
            $('#' + did + ' ul.sdScrollH-item').hover(function (e) {
                if (params.pause_onhover) {
                    if (bEffetContinu) {
                        $("#" + uid).stop();
                        $("#" + uid + "_Clone").stop();
                    }
                    else {
                        $(this).stop();
                    }
                }                
            },
			    function () {
			        if (params.pause_onhover) {
			            var distanceRestante = 0;
			            var tempsRestant = 0;
			            if (bEffetContinu) {
			                var distanceRestanteClone = 0;
			                var tempsRestantClone = 0;
			                if (direction == "+") {
			                    distanceRestante = screen - parseFloat($("#" + uid).css("left"));
			                    tempsRestant = distanceRestante / ((wul + screen) / params.duration);

			                    distanceRestanteClone = screen - parseFloat($("#" + uid + "_Clone").css("left"));
			                    tempsRestantClone = distanceRestanteClone / ((wul + screen) / params.duration);
			                }
			                else {
			                    distanceRestante = wul + parseFloat($("#" + uid).css("left"));
			                    tempsRestant = distanceRestante / ((wul + screen) / params.duration);

			                    distanceRestanteClone = wul + parseFloat($("#" + uid + "_Clone").css("left"));
			                    tempsRestantClone = distanceRestanteClone / ((wul + screen) / params.duration);

			                }
			                if (bScrollHClone) { sdScrollHClone(distanceRestanteClone, tempsRestantClone); }
			                if (bScrollH) { sdScrollH(distanceRestante, tempsRestant); }
			            }
			            else {
			                if (direction == "+") {
			                    distanceRestante = screen - parseFloat($("#" + uid).css("left"));
			                    tempsRestant = distanceRestante / ((wul + screen) / params.duration);
			                }
			                else {
			                    distanceRestante = wul + parseFloat($("#" + uid).css("left"));
			                    tempsRestant = distanceRestante / ((wul + screen) / params.duration);
			                }
			                sdScrollH(distanceRestante, tempsRestant);
			            }
			        }
			    }
            );

            sdScrollH((wul + screen), params.duration); // lance l'animation
            bScrollH = true;
        }
        else {

            //vertical
            switch (params.direction) {
                case "bottom":
                    direction = "+"
                    break;
                case "top":
                    direction = "-"
                    break;
            }


            var bScrollV = false;
            var bScrollVClone = false;

            // Applique la class pour la mise en forme et forcer l'overflow sur le conteneur parent (normalement <div class="columns"></div>)
            if ($(this).parents(".columns").length > 0) { $(this).closest("div.columns").toggleClass("sdScrollV"); }
            else { if ($(this).parents(".column").length > 0) { $(this).closest("div.column").toggleClass("sdScrollV"); } }
            var div = $(this).parents(".sdScrollV")

            $(div).css('height', parseInt($(this).outerHeight(true) + 20) + 'px')

            //Applique la class css sur l'objet <ul>
            $(this).toggleClass("no-bullet").toggleClass("sdScrollV-item");

            if (($(this).attr("id") == "") || ($(this).attr("id") == null)) { $(this).attr("id", "ul_sdScrollV" + "_" + $('.sdScrollV-item').length); }
            if (($(div).attr("id") == "") || ($(div).attr("id") == null)) { $(div).attr("id", "div_sdScrollV" + "_" + $('.sdScrollV').length); }

            did = $(div).attr("id");
            uid = $(this).attr("id");

            var hul = 0; // hauteur de la balise <ul>
            screen = parseFloat($("#" + uid).closest("div.sdScrollV").css("height")); //taille de la div où est affichée le texte défilant

            if ($("#" + uid + ' > li') != null) {
                hul = parseFloat(parseFloat(parseFloat($("#" + uid + ' > li:last').offset().top) - parseFloat($("#" + uid).offset().top)) + parseFloat($("#" + uid + ' > li:last').prop('scrollHeight')));
            }
            $("#" + uid).css("height", (hul) + "px");
            
            $("#" + uid).css("visibility", "visible");
            $("#" + uid).css("top", ((direction == "+") ? "-" + hul : "+" + screen) + "px");

            //options
            //continu
            if (bEffetContinu) {
                //ici le clone
                $("#" + uid).clone().appendTo($("#" + uid).parent());
                $("#" + uid + '.sdScrollV-item:gt(0)').attr("id", uid + "_Clone");
            }
            //bordure
            if (params.fade_border) {
                $("#" + did + ' div[class*="sdScrollV-Bord"]').css("height", parseFloat(screen * 0.2) + "px");
                //il faut placer la div en bas
                $("#" + did + ' div[class*="sdScrollV-BordBas"]').css("top", (parseFloat($(div).css("height")) - (parseFloat($("#" + did + ' div[class="sdScrollV-BordBas"]').css("height")) * 2) - (isNaN(parseFloat($(div).css("border-bottom"))) ? 0 : parseFloat($(div).css("border-bottom"))) - (isNaN(parseFloat($(div).css("border-top"))) ? 0 : parseFloat($(div).css("border-top")))) + "px");

                switch(params.direction)
                {
                    case 'top':
                        $("#" + did + ' div[class="sdScrollV-BordBas"]').css("background", divbg);
                        $("#" + did + ' div[class="sdScrollV-BordHaut"]').css("background", divbg.replace('top', 'bottom'));
                        break;
                    case 'bottom':
                        $("#" + did + ' div[class="sdScrollV-BordHaut"]').css("background", divbg);
                        $("#" + did + ' div[class="sdScrollV-BordBas"]').css("background", divbg.replace('bottom', 'top'));
                        break;
                }

            } else { $("#" + did + ' div[class*="sdScrollV-Bord"]').css("height", "0px"); }
            //fin options

            function sdScrollV(distance, temps) {
                $("#" + uid).animate(
                { "top": direction + "=" + distance + "px" },
                {
                    duration: temps,
                    easing: "linear",
                    step: function (now, fx) {
                        if ((bEffetContinu) && (!bScrollVClone)) {
                            if (direction == "+") {
                                if (now >= 10) { // 20 largeur de marge
                                    sdScrollVClone((hul + screen), params.duration);
                                    bScrollVClone = true;
                                }
                            }
                            else {
                                if (now <= ((screen - 10) - hul)) {
                                    sdScrollVClone((hul + screen), params.duration);
                                    bScrollVClone = true;
                                }
                            }
                        }
                    },
                    complete: function () {
                        $("#" + uid).css("top", ((direction == "+") ? "-" + hul : "+" + screen) + "px");
                        if (bEffetContinu) { bScrollV = false; }
                        else { sdScrollV((hul + screen), params.duration); } //pour la boucle
                    }
                });
            }

            function sdScrollVClone(distance, temps) {
                $("#" + uid + "_Clone").animate(
                { "top": direction + "=" + distance + "px" },
                {
                    duration: temps,
                    easing: "linear",
                    step: function (now, fx) {
                        if ((bEffetContinu) && (!bScrollV)) {
                            if (direction == "+") {
                                if (now >= 10) {
                                    sdScrollV((hul + screen), params.duration);
                                    bScrollV = true;
                                }
                            }
                            else {
                                if (now <= ((screen - 10) - hul)) {
                                    sdScrollV((hul + screen), params.duration);
                                    bScrollV = true;
                                }
                            }
                        }
                    },
                    complete: function () {
                        $("#" + uid + "_Clone").css("top", ((direction == "+") ? "-" + hul : "+" + screen) + "px");
                        bScrollVClone = false;
                    }
                });
            }

            
            $('#' + did + ' ul.sdScrollV-item').hover(function () {
                if (params.pause_onhover) {
                    if (bEffetContinu) {
                        $("#" + uid).stop();
                        $("#" + uid + "_Clone").stop();
                    }
                    else {
                        $(this).stop();
                    }
                }
            },
			    function () {
			        if (params.pause_onhover) {
			            var distanceRestante = 0;
			            var tempsRestant = 0;
			            if (bEffetContinu) {
			                var distanceRestanteClone = 0;
			                var tempsRestantClone = 0;
			                if (direction == "+") {
			                    distanceRestante = screen - parseFloat($("#" + uid).css("top"));
			                    tempsRestant = distanceRestante / ((hul + screen) / params.duration);

			                    distanceRestanteClone = screen - parseFloat($("#" + uid + "_Clone").css("top"));
			                    tempsRestantClone = distanceRestanteClone / ((hul + screen) / params.duration);
			                }
			                else {
			                    distanceRestante = hul + parseFloat($("#" + uid).css("top"));
			                    tempsRestant = distanceRestante / ((hul + screen) / params.duration);

			                    distanceRestanteClone = hul + parseFloat($("#" + uid + "_Clone").css("top"));
			                    tempsRestantClone = distanceRestanteClone / ((hul + screen) / params.duration);

			                }
			                if (bScrollVClone) { sdScrollVClone(distanceRestanteClone, tempsRestantClone); }
			                if (bScrollV) { sdScrollV(distanceRestante, tempsRestant); }
			            }
			            else {
			                if (direction == "+") {
			                    distanceRestante = screen - parseFloat($("#" + uid).css("top"));
			                    tempsRestant = distanceRestante / ((hul + screen) / params.duration);
			                }
			                else {
			                    distanceRestante = hul + parseFloat($("#" + uid).css("top"));
			                    tempsRestant = distanceRestante / ((hul + screen) / params.duration);
			                }
			                sdScrollV(distanceRestante, tempsRestant);
			            }
			        }
			    }
            );
            
            sdScrollV((hul + screen), params.duration); // lance l'animation
            bScrollV = true;            
        }
    });
};
