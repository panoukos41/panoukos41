'use strict'
const themeKey = 'theme-stylesheet';
const urlBase = 'https://cdn.jsdelivr.net/npm/water.css@2/out/';
const stylesheet = document.getElementById(themeKey);

const getTheme = () => window.localStorage.getItem(themeKey);

const setTheme = (theme) => window.localStorage.setItem(themeKey, theme);

const updateTheme = () => {
    const theme = getTheme();
    stylesheet.href = `${urlBase}${theme}.min.css`;
};

const toggleTheme = () => {
    const newTheme = getTheme() === 'light' ? 'dark' : 'light';
    setTheme(newTheme);
    updateTheme();
    return newTheme;
};

if (!getTheme()) {
    const isLight = window.matchMedia('(prefers-color-scheme: light)').matches;
    setTheme(isLight ? 'light' : 'dark');
}

window.themeKey = themeKey;
window.getTheme = getTheme;
window.setTheme = setTheme;
window.updateTheme = updateTheme;
window.toggleTheme = toggleTheme;

updateTheme();