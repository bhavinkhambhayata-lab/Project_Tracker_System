/**
* Template Name: NiceAdmin
* Template URL: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/
* Updated: Apr 20 2024 with Bootstrap v5.3.3
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/

(function () {
    "use strict";

    ///**
    // * Easy selector helper function
    // */
    //const select = (el, all = false) => {
    //  el = el.trim()
    //  if (all) {
    //    return [...document.querySelectorAll(el)]
    //  } else {
    //    return document.querySelector(el)
    //  }
    //}

    ///**
    // * Easy event listener function
    // */
    //const on = (type, el, listener, all = false) => {
    //  if (all) {
    //    select(el, all).forEach(e => e.addEventListener(type, listener))
    //  } else {
    //    select(el, all).addEventListener(type, listener)
    //  }
    //}

    /**
     * Easy on scroll event listener 
     */
    const onscroll = (el, listener) => {
        el.addEventListener('scroll', listener)
    }

    /**
     * Sidebar toggle
     */
    if (document.querySelector('.toggle-sidebar-btn')) {
        document.querySelector('.toggle-sidebar-btn').addEventListener('click', function (e) {
            document.body.classList.toggle('toggle-sidebar');
        });
    }
    if (document.querySelector('.search-bar-toggle')) {
        document.querySelector('.search-bar-toggle').addEventListener('click', function (e) {
            document.querySelector('.search-bar').classList.toggle('search-bar-show');
        });
    }
    //if (select('.toggle-sidebar-btn')) {
    //    on('click', '.toggle-sidebar-btn', function (e) {
    //        select('body').classList.toggle('toggle-sidebar')
    //    })
    //}

    /**
     * Search bar toggle
     */
    //if (select('.search-bar-toggle')) {
    //    on('click', '.search-bar-toggle', function (e) {
    //        select('.search-bar').classList.toggle('search-bar-show')
    //    })
    //}

    /**
     * Navbar links active state on scroll
     */
    //let navbarlinks = select('#navbar .scrollto', true)
    //const navbarlinksActive = () => {
    //    let position = window.scrollY + 200
    //    navbarlinks.forEach(navbarlink => {
    //        if (!navbarlink.hash) return
    //        let section = select(navbarlink.hash)
    //        if (!section) return
    //        if (position >= section.offsetTop && position <= (section.offsetTop + section.offsetHeight)) {
    //            navbarlink.classList.add('active')
    //        } else {
    //            navbarlink.classList.remove('active')
    //        }
    //    })
    //}
    //window.addEventListener('load', navbarlinksActive)
    //onscroll(document, navbarlinksActive)

    let navbarlinks = document.querySelectorAll('#navbar .scrollto');
    const navbarlinksActive = () => {
        let position = window.scrollY + 200;
        navbarlinks.forEach(navbarlink => {
            if (!navbarlink.hash) return;
            let section = document.querySelector(navbarlink.hash);
            if (!section) return;
            if (position >= section.offsetTop && position <= (section.offsetTop + section.offsetHeight)) {
                navbarlink.classList.add('active');
            } else {
                navbarlink.classList.remove('active');
            }
        });
    };

    window.addEventListener('load', navbarlinksActive);
    window.addEventListener('scroll', navbarlinksActive);
    /**
     * Toggle .header-scrolled class to #header when page is scrolled
     */
    //let selectHeader = select('#header')
    //if (selectHeader) {
    //    const headerScrolled = () => {
    //        if (window.scrollY > 100) {
    //            selectHeader.classList.add('header-scrolled')
    //        } else {
    //            selectHeader.classList.remove('header-scrolled')
    //        }
    //    }
    //    window.addEventListener('load', headerScrolled)
    //    onscroll(document, headerScrolled)
    //}
    let selectHeader = document.querySelector('#header');
    if (selectHeader) {
        const headerScrolled = () => {
            if (window.scrollY > 100) {
                selectHeader.classList.add('header-scrolled');
            } else {
                selectHeader.classList.remove('header-scrolled');
            }
        };
        window.addEventListener('load', headerScrolled);
        window.addEventListener('scroll', headerScrolled); // Corrected scroll event
    }
    let selectbody = document.querySelector('#body');
    if (selectbody) {
        const bodyScrolled = () => {
            if (window.scrollY > 100) {
                selectHeader.classList.add('body-scrolled');
            } else {
                selectHeader.classList.remove('body-scrolled');
            }
        };
        window.addEventListener('load', bodyScrolled);
        window.addEventListener('scroll', bodyScrolled); // Corrected scroll event
    }
    /**
     * Back to top button
     */
    let backtotop = document.querySelector('.back-to-top');
    if (backtotop) {
        const toggleBacktotop = () => {
            if (window.scrollY > 100) {
                backtotop.classList.add('active');
            } else {
                backtotop.classList.remove('active');
            }
        };
        window.addEventListener('load', toggleBacktotop);
        window.addEventListener('scroll', toggleBacktotop); // Corrected scroll event
    }

    /**
  * Initiate tooltips
  */
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    /**
     * Initiate quill editors
     */
    if (document.querySelector('.quill-editor-default')) {
        new Quill('.quill-editor-default', {
            theme: 'snow'
        });
    }

    if (document.querySelector('.quill-editor-bubble')) {
        new Quill('.quill-editor-bubble', {
            theme: 'bubble'
        });
    }

    if (document.querySelector('.quill-editor-full')) {
        new Quill(".quill-editor-full", {
            modules: {
                toolbar: [
                    [{
                        font: []
                    }, {
                        size: []
                    }],
                    ["bold", "italic", "underline", "strike"],
                    [{
                        color: []
                    },
                    {
                        background: []
                    }],
                    [{
                        script: "super"
                    },
                    {
                        script: "sub"
                    }],
                    [{
                        list: "ordered"
                    },
                    {
                        list: "bullet"
                    },
                    {
                        indent: "-1"
                    },
                    {
                        indent: "+1"
                    }],
                    ["direction", {
                        align: []
                    }],
                    ["link", "image", "video"],
                    ["clean"]
                ]
            },
            theme: "snow"
        });
    }

    /**
     * Initiate TinyMCE Editor
     */
    const useDarkMode = window.matchMedia('(prefers-color-scheme: dark)').matches;
    const isSmallScreen = window.matchMedia('(max-width: 1023.5px)').matches;

   
    var needsValidation = document.querySelectorAll('.needs-validation');

    Array.prototype.slice.call(needsValidation)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }

                form.classList.add('was-validated');
            }, false);
        });

    /**
     * Initiate Datatables
     */
    const datatables = document.querySelectorAll('.datatable');
    datatables.forEach(datatable => {
        new simpleDatatables.DataTable(datatable, {
            /*   perPageSelect: [5, 10, 15, ["All", -1]],*/
            perPageSelect: [15],
            columns: [{
                select: 2,
                sortSequence: ["desc", "asc"]
            },
            {
                select: 3,
                sortSequence: ["desc"]
            },
            {
                select: 4,
                cellClass: "green",
                headerClass: "red"
            }]
        });
    });

    /**
     * Autoresize echart charts
     */
    const mainContainer = document.querySelector('#main');
    if (mainContainer) {
        setTimeout(() => {
            new ResizeObserver(function () {
                document.querySelectorAll('.echart').forEach(getEchart => {
                    echarts.getInstanceByDom(getEchart).resize();
                });
            }).observe(mainContainer);
        }, 200);
    }
})();