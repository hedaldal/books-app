import { getDisplayName, setDisplayName } from '@/utils/settings';
import { ref } from 'vue';

const profileName = ref(getDisplayName());

export const useProfile = () => {
  const updateProfileName = (name: string) => {
    profileName.value = name;
    setDisplayName(name);
  };

  return { profileName, updateProfileName };
};
