class Categorizer {
    constructor(category) {
        this.category = category;
    }

    categorize() {
        if (this.category === undefined) return;

        let postList = document.querySelector('ul.post-list');
        document.querySelectorAll('ul.post-list li')
            .forEach(post => post.dataset.category !== this.category && postList.removeChild(post));
    }

    static getCategoryFromQueryParameters() {
        const categories = window.location.search
            .substr(1)
            .split('&')
            .map(v => v.split('='));
        return new Map(categories).get('category');
    }
}