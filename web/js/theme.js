const Themes = {
    light: 'light',
    dark: 'dark'
}

// Stylesheets shoud be {url}/theme.min.css where theme is light or dark.
// this.current = window.matchMedia('(prefers-color-scheme: light)').matches
class Theme {
    constructor(stylesheet, url) {
        this.#stylesheet = stylesheet;
        this.key = `${stylesheet.id}.theme`;
        this.url = url.endsWith('/') ? url.slice(0, -1) : url;

        this.current = window.matchMedia('(prefers-color-scheme: light)').matches
            ? Themes.light
            : Themes.dark;

        stylesheet.href = `${url}${this.current}.min.css`;
    }

    get current() {
        return window.localStorage.getItem(key);
    }
    set current(value) {
        window.localStorage.setItem(key, value);
    }

    // Toggles the theme and returns the new theme found in Themes.
    toggle() {
        const theme = this.current === Themes.light ? Themes.dark : Themes.light;
        this.current = theme;
        stylesheet.href = `${url}${theme}.min.css`;
        return theme;
    }
}

export { Themes, Theme }