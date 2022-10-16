function HabilitarMenu()
{
    let displayMenu = document.getElementById("MenuPrincipal");
    let icos = document.getElementById("icosExpand");
    let btnMenu = document.getElementById("btnMenu");

    if (displayMenu.style.display == "none") {
        displayMenu.style.display = "block";
        btnMenu.setAttribute("aria-expanded", "true");
        btnMenu.setAttribute("class", "btn btn-default");
        icos.setAttribute("class", "glyphicon-minus");
    }
    else
    {
        displayMenu.style.display = "none";
        btnMenu.setAttribute("aria-expanded", "false");
        btnMenu.setAttribute("class", "btn btn-default");
        icos.setAttribute("class", "glyphicon-plus");

    }
}