import { getProvider } from "../utilities/provider";

export default function register(...components) {
  components.forEach((Component) => {
    if (Component.config == null) {
      throw new Error("A config needs to be defined for the component");
    }

    const { id, tag } = Component.config;
    const componentId = tag == null ? getProvider(id) : tag;

    /**
     * Initialise the component through html attribute
     */
    const matchedElements = [].slice.apply(
      document.querySelectorAll(componentId)
    );

    for (const el of matchedElements) {
      new Component(el);
    }
  });
}
