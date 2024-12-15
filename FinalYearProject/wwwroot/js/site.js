// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {

    // Sidebar Functionality
    const sidebarToggleBtn = document.getElementById("toggle-btn");
    const sidebar = document.querySelector(".side-navbar");
    const pageHolder = document.querySelector(".page");
    const navbar = document.querySelector('.navbar.fixed-top');

    if (sidebarToggleBtn) {
        sidebarToggleBtn.addEventListener("click", (e) => {
            e.preventDefault();
            sidebar.classList.toggle("shrink");
            pageHolder.classList.toggle("active");
            navbar?.classList.toggle("active");
        });
    }

    window.addEventListener('resize', () => {
        if (window.outerWidth > 1194) {
            sidebar.classList.remove("show-sm");
            pageHolder.classList.remove("active");
            navbar?.classList.remove("active-sm");
        } else {
            sidebar.classList.remove("shrink");
            pageHolder.classList.remove("active");
        }
    });

    // Login Inputs
    let materialInputs = document.querySelectorAll("input.input-material");
    let materialLabels = document.querySelectorAll("label.label-material");

    materialInputs.forEach((input) => {
        let label = input.closest(".input-material-group").querySelector(".label-material");

        if (input.value.trim() !== "") {
            label.classList.add("active");
        }

        input.addEventListener("focus", () => {
            label.classList.add("active");
        });

        input.addEventListener("blur", () => {
            if (input.value.trim() === "") {
                label.classList.remove("active");
            }
        });
    });
});